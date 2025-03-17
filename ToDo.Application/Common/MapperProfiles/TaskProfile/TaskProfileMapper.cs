using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Application.Features.Tasks.Commands.CreateTask;
using ToDo.Application.Features.Tasks.Commands.UpdateTask;
using ToDo.Domain.Entities;

namespace ToDo.Application.Common.MapperProfiles.TaskProfile
{
    public class TaskProfileMapper : Profile
    {
        public TaskProfileMapper()
        {
            CreateMap<CreateTaskCommand, TaskEntity>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Creator))
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false));
            CreateMap<TaskEntity, TaskDTO>();

            CreateMap<UpdateTaskCommand, TaskEntity>()
                .ForMember(dest=>dest.Id,opt=> opt.Ignore())
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        }
    }
}
