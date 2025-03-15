using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Features.Users.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ResultDTO<string>>;

}
