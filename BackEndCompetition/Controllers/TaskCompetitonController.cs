using AutoMapper;
using BackEndCompetition.Helpers;
using CompetitionLibrary.Enums;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCompetition.Controllers
{
    [ApiController]
    [Route("api/TasksCompetition/")]
    public class TaskCompetitonController : ControllerBase
    {
        private readonly IRepository<TaskCompetition> _dbRepositories;
        private readonly ILogger<TaskCompetitonController> _logger;
        private readonly IMapper _mapper;
        private readonly UserHelp _userHelper;

        public TaskCompetitonController(IRepository<TaskCompetition> dbRepositories, ILogger<TaskCompetitonController> logger, IMapper mapper, UserHelp userHelper)
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpGet("mains")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetMainTasksCompetition()
        {
            try
            {
                var dbTasksCompetition = await _dbRepositories.Where(a => a.TaskTypeId == (int)TaskTypeEnum.Main).GetAll();
                var TasksCompetitionAdmin = dbTasksCompetition.Select(taskCompetition => _mapper.Map<TaskCompetitionDto>(taskCompetition)).ToList();
                return new JsonResult(Ok(TasksCompetitionAdmin).Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetTasksCompetition()
        {
            try
            {
                var dbTasksCompetition = await _dbRepositories.GetAll();
                var tasksCompetition = dbTasksCompetition.Select(team => _mapper.Map<TaskCompetitionDto>(team)).ToList();
                return new JsonResult(Ok(tasksCompetition).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> PostTaskCompetition(TaskCompetitionDto newTaskCompetition)
        {
            try
            {
                var taskCompetition = _mapper.Map<TaskCompetition>(newTaskCompetition);
                taskCompetition.CreateTime = DateTime.UtcNow;
                taskCompetition.UpdateTime = DateTime.UtcNow;
                taskCompetition.ObjStatusId = (int)EnumStatus.Active;
                await _dbRepositories.Create(taskCompetition);
                return new JsonResult(Ok("Task is added"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> DeleteTaskCompetition(int taskCompetitionId)
        {
            try
            {
                await _dbRepositories.Delete(taskCompetitionId, await _userHelper.GetId());
                return new JsonResult(Ok("Task was deleted"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }

        }

        [HttpPut]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> UpdateTaskCompetition(TaskCompetitionDto newTaskCompetitionDto)
        {
            try
            {
                var taskCompetition = _mapper.Map<TaskCompetition>(newTaskCompetitionDto);
                await _dbRepositories.Update(taskCompetition.TaskId, taskCompetition);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
