using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        public ToDoController(IToDoRepository todoItems)
        {
            ToDoItems = todoItems;
        }

        public IToDoRepository ToDoItems { get; set; }

        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            return ToDoItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetToDo")]
        public IActionResult GetById(long id)
        {
            var item = ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ToDoItems.Add(item);
            return CreatedAtRoute("GetToDo", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ToDoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.IsComplete = item.IsComplete;

            ToDoItems.Update(todo);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            ToDoItems.Remove(id);
            return new NoContentResult();
        }
    }
}