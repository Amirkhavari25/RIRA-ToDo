using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Queries.GetAllTasks
{
    public record GetAllTaskQuery(string creatorId) : IRequest<ResultDTO<List<TaskDTO>>>;
    
}
