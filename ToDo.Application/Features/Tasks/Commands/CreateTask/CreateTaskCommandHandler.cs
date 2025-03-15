using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ResultDTO<TaskDTO>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<ResultDTO<TaskDTO>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var taskEntity = _mapper.Map<TaskEntity>(request);

                taskEntity.CreateDate = DateTime.Now;
                taskEntity.UpdateDate = DateTime.Now;

                var createdTask = await _taskRepository.CreateAsync(taskEntity);

                var taskDTO = _mapper.Map<TaskDTO>(createdTask);
                return ResultDTO<TaskDTO>.SuccessResult(taskDTO);
            }
            catch (Exception ex)
            {
                return ResultDTO<TaskDTO>.FailureResult($"Error creating task: {ex.Message}");

            }
        }
    }
}
