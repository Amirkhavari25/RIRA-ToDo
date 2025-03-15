using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Contracts.Security
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
