using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Users.Commands.Register
{
    public class RegisterCommand : IRequest<ResultDTO<UserDTO>>
    {
        [Required , MaxLength(100) , EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required, MaxLength(100) , Phone]
        public string Mobile { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
