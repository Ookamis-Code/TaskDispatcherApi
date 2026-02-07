using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskDispatcherApi.Data;
using TaskDispatcherApi.Models;
using TaskDispatcherApi.DTOs;

namespace TaskDispatcherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;
    public TasksController(AppDbContext context) => _context = context;
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll() =>
        await _context.Tasks.OrderByDescending(t => t.Priority == "High").ToListAsync();
    [HttpGet("search")]
    public async Task<ActionResult<List<TaskItem>>> Search(string query)
    {
        return await _context.Tasks
            .Where(t => t.Title.Contains(query) || t.Description.Contains(query))
            .ToListAsync();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TaskCreatDto taskDto)
    {
        var newTask = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            Priority = taskDto.Priority,
            IsDispatched = false
        };
        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = newTask.Id }, newTask);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        task.IsDispatched = !task.IsDispatched;
        await _context.SaveChangesAsync();
        return Ok(task);
    }
}