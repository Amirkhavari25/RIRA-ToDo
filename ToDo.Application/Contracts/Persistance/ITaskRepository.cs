using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Contracts.Persistance
{
    public interface ITaskRepository
    {
        IQueryable<TaskEntity> GetAll();

        Task<TaskEntity?> GetByIdAsync(Guid Id);

        Task<TaskEntity> CreateAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid Id);
    }
}
