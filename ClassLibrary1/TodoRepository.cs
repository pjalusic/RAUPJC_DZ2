using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new List<TodoItem>();
            // Shorter way to write this in C# using ?? operator :
            // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
            // x ?? y -> if x is not null , expression returns x. Else y.
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException();
            if (Get(todoItem.Id) != null)
                throw new DuplicateTodoItemException("duplicate id: {0}", todoItem.Id);
            _inMemoryTodoDatabase.Add(todoItem);
            
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(t => t.Id.Equals(todoId))
                                        .FirstOrDefault();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(t => t.IsCompleted.Equals(false))
                                        .ToList();
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(t => t.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted()
        {

            return _inMemoryTodoDatabase.Where(t => t.IsCompleted)
                                        .ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(t => filterFunction(t)).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem element = Get(todoId);
            if (element == null)
            {
                return false;
            }
            element.MarkAsCompleted();
            return true;
        }

        public bool Remove(Guid todoId)
        {
            TodoItem toRemove = Get(todoId);
            if (toRemove == null)
            {
                return false;
            }
            return _inMemoryTodoDatabase.Remove(toRemove);
            
        }

        public void Update(TodoItem todoItem)
        {
            Remove(todoItem.Id);
            Add(todoItem);
        }
    }
}
