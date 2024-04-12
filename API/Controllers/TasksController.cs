using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;

       public TasksController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetTaskItems()
        {
            try
            {
                return Ok(await _taskItemRepository.GetTaskItemsAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            try
            {
                return await _taskItemRepository.GetTaskItem(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> AddTaskItem(TaskItem taskItem)
        {
            try
            {
                if (taskItem == null)
                return BadRequest();

                var addedTaskItem = await _taskItemRepository.AddTaskItem(taskItem);

                return CreatedAtAction(nameof(GetTaskItem),
                    new { id = addedTaskItem.Id }, addedTaskItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new task item record");
            }
        }

       [HttpPut]
       public async Task<ActionResult<TaskItem>> UpdateTaskItem(TaskItem taskItem)
        {   
            try
            {
                var taskItemToUpdate = await _taskItemRepository.GetTaskItem(taskItem.Id);

                if (taskItemToUpdate == null)
                    return NotFound($"Task Item with Id = {taskItem.Id} not found");

                return await _taskItemRepository.UpdateTaskItem(taskItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

       [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItem>> DeleteTaskItem(int id)
        {
            try
            {
                var taskItem = await _taskItemRepository.GetTaskItem(id);

                if (taskItem == null)
                {
                    return NotFound($"Task Item with Id = {id} not found");
                }

               return await _taskItemRepository.DeleteTaskItem(id);
             
            
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}