using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.Contracts.Security;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultDTO<UserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryption _passwordEncryption;

        public RegisterCommandHandler(IMapper mapper, IUserRepository userRepository,IPasswordEncryption passwordEncryption)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordEncryption = passwordEncryption;
        }
        public async Task<ResultDTO<UserDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkUserExist = await _userRepository.GetByEmailAsync(request.Email);
                if(checkUserExist != null)
                {
                    return ResultDTO<UserDTO>.FailureResult("User already exist try to login");
                }
                var UserEntity = _mapper.Map<User>(request);
                UserEntity.PasswordHash = await _passwordEncryption.HashPassword(request.Password);
                await _userRepository.CreateAsync(UserEntity);
                var userDTO = _mapper.Map<UserDTO>(UserEntity);
                return ResultDTO<UserDTO>.SuccessResult(userDTO);
            }
            catch (Exception ex)
            {
                return ResultDTO<UserDTO>.FailureResult($"Error user registration: {ex.Message}");
            }
        }
    }
}
