using System;
using System.Collections.Generic;

namespace Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
        }

        public void MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
            }
        }

        private sealed class IdEqualityComparer : IEqualityComparer<TodoItem>
        {
            public bool Equals(TodoItem x, TodoItem y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(TodoItem obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private static readonly IEqualityComparer<TodoItem> IdComparerInstance = new IdEqualityComparer();

        public static IEqualityComparer<TodoItem> IdComparer
        {
            get { return IdComparerInstance; }
        }

    }
}
