using EmployeeApp.Api.Models.Dtos;
using EmployeeApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result=await service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await service.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var result=await service.AddAsync(entity);
            if (result is null) return NotFound();
            return CreatedAtAction("GetById", new { id = result.Id }, result);

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await service.UpdateAsync(id, entity);
            if (result is null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result=await service.DeleteAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }


    }
}
