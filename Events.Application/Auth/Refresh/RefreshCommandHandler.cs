using Events.Application.Auth.Login;
using Events.Application.Common.Exceptions;
using Events.Application.Interfaces;
using Events.Domain;
using Events.Persistence.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Auth.Refresh
{
    public class RefreshCommandHandler
        : IRequestHandler<RefreshCommand, JwtTokensDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public RefreshCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) =>
            (_unitOfWork, _jwtProvider) = (unitOfWork, jwtProvider);

        public async Task<JwtTokensDto> Handle(RefreshCommand request,
            CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var user = await _unitOfWork.UserRepository().GetByRefreshToken(request.RefreshToken, cancellationToken);
            var refToken = tokenHandler.ReadToken(request.RefreshToken);

            if (refToken.ValidTo > DateTime.Now && user != null)
            {
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
            else
            {
                throw new Exception("Refresh error");
            }


        }

    }
}
