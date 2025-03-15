using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Contracts.Security
{
    public interface IPasswordEncryption
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hashedPassword);
    }
}
