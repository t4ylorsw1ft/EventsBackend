using AutoMapper;
using Events.Application.Auth.Login;
using Events.Application.Auth.Refresh;
using Events.Application.Interfaces;

using Events.Persistence.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Events.WebApi.Middleware
{
    public static class AuthenticationMiddleware
    {
        public static void AddApiAuthentication(
            this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
        {

        var tokenHandler = new JwtSecurityTokenHandler();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>    
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))

                    };


                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context =>
                        {
                            var accessToken = context.Request.Cookies["access-token"];
                            var refreshToken = context.Request.Cookies["refresh-token"];

                            context.Token = accessToken;
                            await Task.CompletedTask;
                        },
                        OnChallenge = async context =>
                        {
                            if (context.AuthenticateFailure is SecurityTokenExpiredException)
                            {
                                throw new Exception("Token has expired");
                            }
                            else
                            {
                                throw new Exception("Token validation failure");
                            }
                        }
                    };

                });
            services.AddAuthorization();
        }
    }
}
