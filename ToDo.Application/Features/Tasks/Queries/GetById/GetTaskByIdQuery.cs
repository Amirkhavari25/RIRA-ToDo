using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Queries.GetById
{
    public record GetTaskByIdQuery(Guid id) : IRequest<ResultDTO<TaskDTO>>;
    
}
