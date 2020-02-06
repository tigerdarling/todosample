using System.Collections.Generic;

namespace ToDoApi.Models
{
    public interface IToDoRepository
    {
        void Add(ToDoItem item);
        IEnumerable<ToDoItem> GetAll();
        ToDoItem Find(long key);
        void Remove(long key);
        void Update(ToDoItem item);
    }
}