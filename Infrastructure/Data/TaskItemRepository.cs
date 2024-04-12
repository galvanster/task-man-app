using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class TaskItemRepository : ITaskItemRepository
  {
    private readonly AppDbContext _context;
    
    public TaskItemRepository(AppDbContext context)
    {   
      _context = context;
    
    }

    public async Task<TaskItem> AddTaskItem(TaskItem taskItem)
    {
       var result = await _context.AddAsync(taskItem);
       await _context.SaveChangesAsync();
       return result.Entity;
    }

    public async Task<TaskItem> DeleteTaskItem(int id)
    {
      var result = await _context.TaskItem.FindAsync(id);

      if(result != null)
      {
        _context.TaskItem.Remove(result);
        await _context.SaveChangesAsync();
        return result;
      }
      return null;
    }

    public async Task<TaskItem> GetTaskItem(int id)
    {
      return await _context.TaskItem.FindAsync(id);
    }

    public async Task<IReadOnlyList<TaskItem>> GetTaskItemsAsync()
    {
      return await _context.TaskItem.ToListAsync();
    }

    public async Task<TaskItem> UpdateTaskItem(TaskItem taskItem)
    {
      var result = await _context.TaskItem.FindAsync(taskItem.Id);

      if(result != null)
      {
        result.Description = taskItem.Description;
        await _context.SaveChangesAsync();
        return result;
      }
      return null;
    }
  }
}