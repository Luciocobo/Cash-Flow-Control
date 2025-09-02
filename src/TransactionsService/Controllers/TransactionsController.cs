using Microsoft.AspNetCore.Mvc;
using TransactionsService.Application.Services;
using TransactionsService.Application.Dto;

namespace TransactionsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
    {
        var id = await _service.CreateAsync(dto);
        var transaction = await _service.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, transaction);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var t = await _service.GetByIdAsync(id);
        if (t == null) return NotFound();
        return Ok(t);
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var items = await _service.GetByDateAsync(date);
        return Ok(items);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

}