using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Set<TaskItem>().ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            return await _context.Set<TaskItem>().FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Set<TaskItem>().AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Set<TaskItem>().Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                _context.Set<TaskItem>().Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
