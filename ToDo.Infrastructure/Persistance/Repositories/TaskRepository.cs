using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Persistance;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Persistance.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteAsync(Guid Id)
        {
            var task = await _context.Tasks.FindAsync(Id);
            if (task != null)
            {
                task.IsDelete = true;
                _context.Update(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskEntity?>> GetAllAsync(string creator)
        {
            IQueryable<TaskEntity> query = _context.Tasks.Where(t => !t.IsDelete && t.UserId.ToString() == creator).AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid Id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == Id);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            if (!task.IsDelete)
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
