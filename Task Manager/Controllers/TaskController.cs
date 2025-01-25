using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Interfaces;
using Task_Manager.Models;
using Task_Manager.ViewModels;

namespace Task_Manager.Controllers
{
    //[Authorize]
    public class TaskController : Controller
    {
        private readonly IRepository<Duty> _taskRepository;
        private readonly IRepository<Status> _statusRepository;
        private readonly IValidator<DutyCreateModel> _dutyCreateModelValidator;

        public TaskController(IRepository<Duty> taskRepository, IRepository<Status> statusRepository, IValidator<DutyCreateModel> dutyCreateModelValidator)
        {
            _taskRepository = taskRepository;
            _statusRepository = statusRepository;
            _dutyCreateModelValidator = dutyCreateModelValidator;
        }

        
        public async Task<IActionResult> Index()
        {
            var data = await _taskRepository.GetQuery().Where(x => !x.IsArchived).ToListAsync();
            
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var model = new DutyCreateModel();
            var statuses = await _statusRepository.GetAll();
            model.Status = new SelectList(statuses,"Id","Name");
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(DutyCreateModel duty)
        {
            var result = _dutyCreateModelValidator.Validate(duty);

            if (result.IsValid)
            {
                var dutyEntity = new Duty
                {
                    Name = duty.Name,
                    Description = duty.Description,
                    StatusId = duty.StatusId,
                    DueDate = duty.DueDate
                };

                await _taskRepository.Create(dutyEntity);

                return RedirectToAction("Index");
            }

            var statuses = await _statusRepository.GetAll();
            duty.Status = new SelectList(statuses, "Id", "Name", duty.StatusId);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(duty);

        }

        public async Task<IActionResult> Update(int id)
        {
            
            var statuses = await _statusRepository.GetAll();

            var duty = await _taskRepository.GetByFilter(x => x.Id == id);
            var model = new DutyCreateModel
            {
                Id = duty.Id,
                Name = duty.Name,
                Description = duty.Description,
                StatusId = duty.StatusId,
                Status = new SelectList(statuses, "Id", "Name", duty.StatusId),
                DueDate = duty.DueDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DutyCreateModel duty)
        {

            var entity = await _taskRepository.Find(duty.Id);

            if(entity == null)
            {
                return NotFound();
            }

            var result = _dutyCreateModelValidator.Validate(duty);

            if (result.IsValid)
            {
                entity.Name = duty.Name;
                entity.Description = duty.Description;
                entity.StatusId = duty.StatusId;
                entity.DueDate = duty.DueDate;

                await _taskRepository.Update(entity);

                return RedirectToAction("Index");
            }

            var statuses = await _statusRepository.GetAll();
            duty.Status = new SelectList(statuses, "Id", "Name", duty.StatusId);

            return View(duty);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _taskRepository.Find(id);
            entity.IsArchived = true;
            await _taskRepository.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
