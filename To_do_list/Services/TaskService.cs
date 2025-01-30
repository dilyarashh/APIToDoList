using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using To_do_list.Data;
using To_do_list.Data.DTO;
using To_do_list.Data.Entities;
using To_do_list.Services.IServices;
using Microsoft.EntityFrameworkCore;
using To_do_list.Data.Entities.Enums;

namespace To_do_list.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDBContext _dbContext;

        public TaskService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTask(AddTaskDTO tasksDto)
        {
            if (tasksDto == null || tasksDto.Description == "")
            {
                throw new BadHttpRequestException("Текст пустой!");
            }

            Tasks task = new Tasks()
            {
                Description = tasksDto.Description,
                Status = Statuses.Not_Completed
            };

            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteTask(Guid id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new BadHttpRequestException("Задачи нет!");
            }

            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditTask(EditTaskDTO editTaskDTO)
        {
            if (editTaskDTO == null || editTaskDTO.Description == "")
            {
                throw new BadHttpRequestException("Пустой текст!");
            }

            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == editTaskDTO.Id);

            if (task == null)
            {
                throw new BadHttpRequestException("Задачи нет!");
            }

            task.Description = editTaskDTO.Description;

            await _dbContext.SaveChangesAsync();
        }
        public async Task ChangeStatus(Guid id, Statuses status)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(i => i.Id == id);
            if (task == null)
            {
                throw new BadHttpRequestException("Задачи нет!");
            }

            task.Status = status;
            await _dbContext.SaveChangesAsync();
        }
        public async Task <List<TaskDTO>> GetAllTasks()
        {
            var tasks = await _dbContext.Tasks.ToListAsync();
            var allTasks = tasks.Select(i => new TaskDTO { 
                    id = i.Id,
                    Description = i.Description,
                    Status = i.Status
                }
            ).ToList();
            return allTasks;
        }

        public async Task LoadTasks(List<LoadDTO> newTasks)
        {
            var allTasks = await _dbContext.Tasks.ToListAsync();
            _dbContext.Tasks.RemoveRange(allTasks);
            
            if (newTasks == null)
            {
                throw new BadHttpRequestException("Список задач пустой!");
            }

            foreach (var newTask in newTasks)
            {
                if (string.IsNullOrEmpty(newTask.Description))
                {
                    throw new BadHttpRequestException("У задачи пустой текст!");
                }
                
                Tasks task = new Tasks()
                {
                    Description = newTask.Description,
                    Status = newTask.Status
                };
                _dbContext.Tasks.Add(task);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}