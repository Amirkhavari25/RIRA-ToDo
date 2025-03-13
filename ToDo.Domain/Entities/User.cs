using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ToDo.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
