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

namespace ToDo.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ResultDTO<string>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<ResultDTO<string>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(request.Id);
                if (task == null)
                {
                    return ResultDTO<string>.FailureResult("Task not found");
                }
                var updatedTask = _mapper.Map(request, task);
                updatedTask.UpdateDate = DateTime.Now;

                await _taskRepository.UpdateAsync(updatedTask);
                return ResultDTO<string>.SuccessResult("Successfuly updated");

            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult(ex.Message);
            }
        }
    }
}
