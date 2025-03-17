using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Queries.GetAllTasks
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTaskQuery, ResultDTO<List<TaskDTO>>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public GetAllTasksQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<ResultDTO<List<TaskDTO>>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync(request.creatorId);

                var taskDTOs = _mapper.Map<List<TaskDTO>>(tasks);

                return ResultDTO<List<TaskDTO>>.SuccessResult(taskDTOs);

            }
            catch (Exception ex)
            {
                return ResultDTO<List<TaskDTO>>.FailureResult($"Error getting all tasks: {ex.Message}");
            }
        }
    }
}
