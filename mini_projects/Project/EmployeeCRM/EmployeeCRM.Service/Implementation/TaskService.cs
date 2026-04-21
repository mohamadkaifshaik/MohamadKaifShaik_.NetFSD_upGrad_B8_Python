using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;


namespace EmployeeCRM.Service.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        // Get all tasks
        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _repository.GetAllAsync();
                return tasks.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all tasks", ex);
            }
        }

        // Get task by ID
        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid task ID");

                var task = await _repository.GetByIdAsync(id);

                if (task == null)
                    throw new KeyNotFoundException($"Task with ID {id} not found");

                return MapToDTO(task);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving task with ID {id}", ex);
            }
        }

        // Create new task
        public async Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO)
        {
            try
            {
                // Validation
                if (taskDTO == null)
                    throw new ArgumentNullException(nameof(taskDTO));

                if (string.IsNullOrWhiteSpace(taskDTO.Title))
                    throw new ArgumentException("Task title is required");

                if (taskDTO.DueDate < DateTime.Now)
                    throw new ArgumentException("Due date must be in the future");

                if (taskDTO.Priority < 1 || taskDTO.Priority > 3)
                    throw new ArgumentException("Priority must be between 1 and 3");

                // Map DTO to entity
                var task = new TaskItem
                {
                    Title = taskDTO.Title,
                    Description = taskDTO.Description,
                    Status = TaskStatusEnum.Pending, // Watch out
                    Priority = taskDTO.Priority,
                    DueDate = taskDTO.DueDate,
                    EmployeeId = taskDTO.EmployeeId,
                    ClientId = taskDTO.ClientId,
                    ProgressPercentage = 0,
                    CreatedDate = DateTime.Now
                };

                var result = await _repository.AddAsync(task);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating task", ex);
            }
        }

        // Update task
        public async Task<TaskDTO> UpdateTaskAsync(int id, TaskDTO taskDTO)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid task ID");

                if (taskDTO == null)
                    throw new ArgumentNullException(nameof(taskDTO));

                var task = await _repository.GetByIdAsync(id);

                if (task == null)
                    throw new KeyNotFoundException($"Task with ID {id} not found");

                // Update properties
                task.Title = taskDTO.Title;
                task.Description = taskDTO.Description;
                task.Status = taskDTO.Status;
                task.Priority = taskDTO.Priority;
                task.DueDate = taskDTO.DueDate;
                task.ProgressPercentage = taskDTO.ProgressPercentage;
                task.ClientId = taskDTO.ClientId;
                task.UpdatedDate = DateTime.Now;

                // If completing task, set completion date
                if (taskDTO.Status == TaskStatusEnum.Completed && task.CompletedDate == null)
                    task.CompletedDate = DateTime.Now;

                var result = await _repository.UpdateAsync(task);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating task with ID {id}", ex);
            }
        }

        // Delete task
        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid task ID");

                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting task with ID {id}", ex);
            }
        }

        // Get tasks by employee
        public async Task<IEnumerable<TaskDTO>> GetTasksByEmployeeAsync(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                    throw new ArgumentException("Invalid employee ID");

                var tasks = await _repository.GetByEmployeeAsync(employeeId);
                return tasks.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks by employee", ex);
            }
        }

        // Get tasks by status
        public async Task<IEnumerable<TaskDTO>> GetTasksByStatusAsync(TaskStatusEnum status)
        {
            try
            {
                var tasks = await _repository.GetByStatusAsync(status);
                return tasks.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks by status", ex);
            }
        }

        // Get overdue tasks
        public async Task<IEnumerable<TaskDTO>> GetOverdueTasksAsync()
        {
            try
            {
                var tasks = await _repository.GetOverdueTasksAsync();
                return tasks.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving overdue tasks", ex);
            }
        }

        // Helper method: Map Entity to DTO
        private TaskDTO MapToDTO(TaskItem task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                EmployeeId = task.EmployeeId,
                ClientId = task.ClientId,
                ProgressPercentage = task.ProgressPercentage,
                CreatedDate = task.CreatedDate,
                CompletedDate = task.CompletedDate
            };
        }
    }
}