using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Queries.GetById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ResultDTO<TaskDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<ResultDTO<TaskDTO>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tasksEntities = await _taskRepository.GetByIdAsync(request.id);
                var result = _mapper.Map<TaskDTO>(tasksEntities);
                return ResultDTO<TaskDTO>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ResultDTO<TaskDTO>.FailureResult($"Error getting task detail:{ex.Message}");
            }
        }
    }
}
