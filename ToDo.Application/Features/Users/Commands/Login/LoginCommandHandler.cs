using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.Contracts.Security;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultDTO<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _jwtTokenService;
        private readonly IPasswordEncryption _passwordEncryption;

        public LoginCommandHandler(IUserRepository userRepository, ITokenService jwtTokenService, IPasswordEncryption passwordEncryption)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _passwordEncryption = passwordEncryption;
        }
        public async Task<ResultDTO<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //check if user exist
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return ResultDTO<string>.FailureResult("User not found");
            }
            //check password
            if (!await _passwordEncryption.VerifyPassword(request.Password, user.PasswordHash))
            {
                return ResultDTO<string>.FailureResult("Wrong password or email");
            }
            try
            {
                //generate token
                var token = await _jwtTokenService.CreateToken(user);
                //return token
                return ResultDTO<string>.SuccessResult(token);
            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult($"login error : {ex.Message}");
            }
        }
    }
}
