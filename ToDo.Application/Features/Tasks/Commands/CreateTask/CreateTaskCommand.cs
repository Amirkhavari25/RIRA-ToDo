using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest<ResultDTO<TaskDTO>>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        [JsonIgnore]
        public Guid Creator { get; set; }
    }
    
    
}
