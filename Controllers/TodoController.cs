using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using taskApi.Data;
using taskApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace taskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet("get-tasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                // Retrieve the user ID of the currently logged-in user
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Retrieve only the tasks belonging to the user
                var tasks = await _context.Todos
                    .Where(t => t.UserId == userId)
                    .ToListAsync();

                return Ok(tasks);
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return Ok();
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        private async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Todos.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/Task
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask(Todo todo)
        {
            // You can access the authenticated user's information from User.Identity
            var userIdClaim = User.FindFirst("sub");
            if (userIdClaim == null)
            {
                return Unauthorized(); // User claim not found in token
            }

            // You can use userIdClaim.Value to get the user's ID

            // Set the user ID for the task
            // For demonstration, let's assume you have a UserId property in the Todo model
            todo.UserId = Convert.ToInt32(userIdClaim.Value);

            // Add the task to the database
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            // Return the created task with HTTP status 201
            return CreatedAtAction(nameof(GetTask), new { id = todo.Id }, todo);
        }

        // PUT: api/Task/5
        [HttpPut("update-task/{id}")]
        public async Task<IActionResult> UpdateTask(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Task/5
        [HttpDelete("delete-task/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Todos.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}