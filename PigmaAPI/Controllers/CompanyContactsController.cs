using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PigmaAPI.Common.Enums;
using PigmaAPI.Entities;
using PigmaAPI.Infrastructure.ApplicationDbContext;
using PigmaAPI.Services.CompanyContacts.Services.Contracts;

namespace PigmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyContactsController : ControllerBase
    {
        private readonly ICompanyContactService _service;
        private readonly ILogger<CompanyContactsController> _logger;

        public CompanyContactsController(ICompanyContactService service, ILogger<CompanyContactsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyContact>>> GetAllCompanyContacts()
        {
            _logger.LogInformation("{ControllerBase} GetAllCompanyContacts()", nameof(ControllerBase));

            var result = await _service.GetAll();
            if (result != null)
            {
              return Task.FromResult(result).Result;
            }
            return NotFound();
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public async Task<ActionResult<CompanyContact>> GetCompanyContactById(int id)
        {
            _logger.LogInformation("{id},{ControllerBase} GetCompanyContactById()", nameof(id), nameof(ControllerBase));

            var result = await _service.GetById(id);

            if (result != null)
            {
                return Task.FromResult(result).Result;
            }

            return NotFound();
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> PutCompanyContact(CompanyContact companyContact)
        {
            _logger.LogInformation("{CompanyContact},{ControllerBase} PutCompanyContact()", nameof(CompanyContact), nameof(ControllerBase));

            var result = await _service.Update(companyContact);
            if (result is ActionStatus.Success)
            {
                return Ok();
            }
            
            return NotFound();
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> PostCompanyContact(CompanyContact companyContact)
        {
            _logger.LogInformation("{CompanyContact},{ControllerBase} PostCompanyContact()", nameof(CompanyContact), nameof(ControllerBase));

            var result = await _service.Create(companyContact);
            if (result is ActionStatus.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyContact(int id)
        {
            _logger.LogInformation("{id},{ControllerBase} DeleteCompanyContact()", nameof(id), nameof(ControllerBase));

            var result = await _service.DeleteById(id);
            if (result is ActionStatus.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

     
    }
}
