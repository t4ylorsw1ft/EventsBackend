using Events.Application.Common.Exceptions;
using Events.Application.Interfaces;
using Events.Persistence.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Auth.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, JwtTokensDto>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) =>
            (_unitOfWork, _passwordHasher, _jwtProvider) = (unitOfWork, passwordHasher, jwtProvider);

        public async Task<JwtTokensDto> Handle(LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository().GetByEmail(request.Email, cancellationToken);

            var verification = _passwordHasher.Verify(request.Password, user.Password);

            if (verification == false)
            {
                throw new LoginFailException();
            }

            var tokens = new JwtTokensDto
            {
                AccessToken = _jwtProvider.GenerateAccessToken(user),
                RefreshToken = _jwtProvider.GenerateRefreshToken()
            };

            user.RefreshToken = tokens.RefreshToken;

            await _unitOfWork.UserRepository().Update(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return tokens;
        }
    }
}
