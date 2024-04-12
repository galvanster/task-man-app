using Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IReadOnlyList<TaskItem>> GetTaskItemsAsync();
        Task<TaskItem> GetTaskItem(int id);
        Task<TaskItem> AddTaskItem(TaskItem taskItem);
        Task<TaskItem> UpdateTaskItem(TaskItem taskItem);
        Task<TaskItem> DeleteTaskItem(int id);
    }
}