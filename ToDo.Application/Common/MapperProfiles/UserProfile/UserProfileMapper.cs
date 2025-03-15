using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Application.Features.Users.Commands.Register;
using ToDo.Domain.Entities;

namespace ToDo.Application.Common.MapperProfiles.UserProfile
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<User, UserDTO>();

        }
    }
}
