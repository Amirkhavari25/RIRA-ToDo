using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ResultDTO<string>>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<ResultDTO<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(request.id);
                if (task == null)
                {
                    return ResultDTO<string>.FailureResult("Task not found");
                }
                await _taskRepository.DeleteAsync(request.id);
                return ResultDTO<string>.SuccessResult("Task deleted successful");
            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult(ex.Message);
            }
        }
    }
}
