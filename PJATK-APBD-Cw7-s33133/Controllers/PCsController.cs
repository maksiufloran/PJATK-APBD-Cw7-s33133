using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw7_s33133.DTOs;
using PJATK_APBD_Cw7_s33133.Services;

namespace PJATK_APBD_Cw7_s33133.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IPCService _service;

    public PCsController(IPCService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pcs = await _service.GetAllPCsAsync();
        return Ok(pcs); // 200 OK
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetById(int id)
    {
        var pc = await _service.GetPCWithComponentsAsync(id);
        if (pc == null)
        {
            return NotFound(); // 404 Not Found
        }
        return Ok(pc); // 200 OK
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PCRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 Bad Request
        }

        var createdPc = await _service.CreatePCAsync(dto);
        // Zwraca 201 Created
        return CreatedAtAction(nameof(GetById), new { id = createdPc.Id }, createdPc); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PCRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 Bad Request
        }

        var updated = await _service.UpdatePCAsync(id, dto);
        if (!updated)
        {
            return NotFound(); // 404 Not Found
        }

        return Ok(dto); // 200 OK (według pdf często zwraca się zaaktualizowany obiekt lub No Content)
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeletePCAsync(id);
        if (!deleted)
        {
            return NotFound(); // 404 Not Found
        }

        return NoContent(); // 204 No Content
    }
}