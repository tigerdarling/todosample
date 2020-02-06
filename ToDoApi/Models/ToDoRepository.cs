using System.Linq;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
            if( _context.ToDoItems.Count() == 0)
            {
                Add(new ToDoItem { Name = "Cook toothpaste." });
                Add(new ToDoItem { Name = "Polish the dog." });
                Add(new ToDoItem { Name = "Shred any spare cash." });
                _context.SaveChanges();
            }
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _context.ToDoItems.ToList();
        }

        public void Add(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
        }

        public ToDoItem Find(long key)
        {
            return _context.ToDoItems.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.ToDoItems.First(t => t.Key == key);
            _context.ToDoItems.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            _context.SaveChanges();
        }
    }
}