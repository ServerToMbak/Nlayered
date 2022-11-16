using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand:IRequest<LoggedDto>
    {
        public UserForLoginDto userForLoginDto {get;set;}
        public string IpAddress { get;set;}


        public class LoginCommandHandler:IRequestHandler<LoginCommand,LoggedDto> 
        {
            private readonly IUserService _userService;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                _userService = userService;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
              User user =await _userService.GetByEmail(request.userForLoginDto.Email);
                await _authBusinessRules.UserShouldBeExist(user);
                await _authBusinessRules.UserShouldBeMatch(user.Id, request.userForLoginDto.Password);

                LoggedDto loggedDto= new LoggedDto();

                if(user.AuthenticatorType is )



            }
        }
    }
}
