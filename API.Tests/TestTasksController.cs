using API.Controllers;
using Core.Entities;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;


namespace API.Tests
{
    public class TestTasksController
    {
        [Fact]
        public async Task GetTaskItems_ShouldTestGetTaskItems()
        {
            // Arange
            var mockRepo = new Mock<ITaskItemRepository>();
            mockRepo.Setup(repo => repo.GetTaskItemsAsync())
                .ReturnsAsync(GetTestTaskItems());
            var controller = new TasksController(mockRepo.Object);

            // Act
            var actionResult = await controller.GetTaskItems();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<TaskItem>;
                
            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetTestTaskItems().Count(), actual.Count());
        }
    
        [Fact]
        public async Task GetTaskItem_ShouldTestGetTaskById()
        {
            // Arange
            var mockRepo = new Mock<ITaskItemRepository>();
            mockRepo.Setup(repo => repo.GetTaskItem(It.IsAny<int>()))
                .ReturnsAsync(new TaskItem {Id =1, Description = "Task One"});
            var controller = new TasksController(mockRepo.Object);

            // Act
            var actionResult = await controller.GetTaskItem(1);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as TaskItem;

            //Assert
            Assert.IsType<TaskItem>(actual);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, actual.Id);
            Assert.Equal("Task One", actual.Description);                                                                                                                                                                             
        }
    
        [Fact]
        public async Task AddTaskItem_ShouldTestAddNewTaskItem()
        {
            // Arange
            var taskList = GetTestTaskItems();
            var mockRepo = new Mock<ITaskItemRepository>();
            mockRepo.Setup(repo => repo.AddTaskItem(taskList[2]))
                .ReturnsAsync(taskList[2]);
            var controller = new TasksController(mockRepo.Object);

            // Act
            var actionResult = await controller.AddTaskItem(taskList[2]);
            var result = actionResult.Result;

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);                                                                                                                                                    
        }

        [Fact]
        public async Task UpdateTaskItem_ShouldTestUpdatingTaskItem()
        {
            // Arange
            TaskItem t = new TaskItem()
            {
                Id = 1,
                Description = "Task One Updated"
            };
              var mockRepo = new Mock<ITaskItemRepository>();
            mockRepo.Setup(repo => repo.GetTaskItem(It.IsAny<int>()))
                .ReturnsAsync(new TaskItem {Id =1, Description = "Task One"});
            var controller = new TasksController(mockRepo.Object);
              
            // Act  
            var actionResult = await controller.UpdateTaskItem(t.Id, t);
            var result = actionResult.Result as OkObjectResult;
           
            //Assert
            Assert.IsType<OkObjectResult>(result);                                                                                                            
        }

        [Fact]
        public async Task DeleteTaskItem_ShouldTestDeletionTaskItem()
        {
            // Arange
            var taskList = GetTestTaskItems();
              var mockRepo = new Mock<ITaskItemRepository>();
            mockRepo.Setup(repo => repo.GetTaskItem(It.IsAny<int>()))
                .ReturnsAsync(taskList[2]);
            var controller = new TasksController(mockRepo.Object);
              
            // Act  
            var actionResult = await controller.DeleteTaskItem(taskList[2].Id);
            var result = actionResult.Result as OkObjectResult;
            
            //Assert
            Assert.IsType<OkObjectResult>(result);                                                                                                            
        }
                                                                                                                                                    

        private List<TaskItem> GetTestTaskItems()
        {
            var tasks = new List<TaskItem>();
            tasks.Add(new TaskItem()
            {
                Id = 1,
                Description = "Task One"
            });
            tasks.Add(new TaskItem()
            {
                Id = 2,
                Description = "Task Two"
            });
            tasks.Add(new TaskItem()
            {
                Id = 3,
                Description = "Task Three"
            });
            return tasks;
        }
    }
}