using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Contracts.Persistance
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task CreateAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
