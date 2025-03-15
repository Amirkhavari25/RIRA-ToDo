using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using ToDo.Application.Contracts.Security;

namespace ToDo.Infrastructure.Security
{
    public class PasswordEncryption : IPasswordEncryption
    {
        public Task<string> HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return Task.FromResult(hashedPassword);
        }

        public Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return Task.FromResult(isValid);
        }
    }
}
