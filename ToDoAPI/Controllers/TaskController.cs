
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers;

[ApiController]
//[Route("[controller]")]
[Route("/")]
[Microsoft.AspNetCore.Cors.EnableCors("MyAllowSpecificOrigins")]
public class TaskController : Controller
{
    public readonly Context _context;

    public TaskController(Context context)
    {
        _context = context;
    }

    [HttpPost("AddTask")]
    public Models.Task AddTask(Models.Task tasks)
    {
        _context.Add(tasks);
        _context.SaveChanges();
        return tasks;
    }

    [HttpGet("GetTasks")]
    public IEnumerable<Models.Task> GetAllActiveTasks()
    {
        return _context.Tasks.Where(x => x.Completed == false && x.Deleted == false);
    }

    [HttpPost("UpdateTask")]
    public Models.Task UpdateTask(Models.Task task)
    {
        _context.Update(task);
        _context.SaveChanges();
        return task;
    }
}
