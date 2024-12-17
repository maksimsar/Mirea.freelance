using Microsoft.EntityFrameworkCore;
using Mirea.Freelance.backend.data;

namespace Mirea.Freelance.backend.services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Task> CreateTaskAsync(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.ClientProfile)
                .Include(t => t.FreelancerProfile)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Task?> UpdateTaskAsync(int id, Task updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return null;

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.Budget = updatedTask.Budget;
            existingTask.Deadline = updatedTask.Deadline;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}