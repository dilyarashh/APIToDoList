using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_do_list.Data.DTO;
using To_do_list.Data.Entities.Enums;
using To_do_list.Services.IServices;

namespace To_do_list.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDTO tasksDto)
        {
            await _service.AddTask(tasksDto);
            return Ok(); 
        }
        
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _service.DeleteTask(id); 
            return Ok();
        }
        
        [HttpPatch("Edit")]
        public async Task<IActionResult> EditTask([FromBody] EditTaskDTO editTaskDTO)
        {
            await _service.EditTask(editTaskDTO);
            return Ok();
        }

        [HttpPatch("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromQuery] Guid id, Statuses status)
        {
            if (!Enum.IsDefined(typeof(Statuses), status))
            {
                return BadRequest("Недопустимый статус.");
            }

            await _service.ChangeStatus(id, status);
            return Ok();
        }
        
        [HttpGet("Get")]
        public async Task<ActionResult<List<TaskDTO>>> GetAllTasks()
        {
            var result = await _service.GetAllTasks();
            return Ok(result);
        }
        
        [HttpPost("Load")]
        public async Task<IActionResult> ReplaceAllTasks([FromBody] List<LoadDTO> newTasks)
        {
            await _service.LoadTasks(newTasks);
            return Ok();
        }
    }
}