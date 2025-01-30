using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using To_do_list.Data.DTO;
using To_do_list.Data.Entities.Enums;

namespace To_do_list.Services.IServices
{
    public interface ITaskService
    {
        public Task AddTask(AddTaskDTO tasksDto);
        public Task DeleteTask(Guid id);
        
        public Task EditTask(EditTaskDTO editTaskDTO);
        public Task ChangeStatus(Guid id, Statuses status);
        
        public Task<List<TaskDTO>> GetAllTasks();
        public Task LoadTasks(List<LoadDTO> newTasks);
    }
}