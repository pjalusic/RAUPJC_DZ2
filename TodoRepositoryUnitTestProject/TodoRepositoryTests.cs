using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Models
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GettingActiveAndCompletedTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem("Groceries");
            repository.Add(todoItem1);
            repository.Add(todoItem2);
            List<TodoItem> active = repository.GetActive();
            bool isCompleted1 = repository.MarkAsCompleted(todoItem1.Id);
            bool isCompleted2 = repository.MarkAsCompleted(todoItem2.Id);
            List<TodoItem> completed = repository.GetCompleted();
            List<TodoItem> currentlyActive = repository.GetActive();
            Assert.IsTrue(isCompleted1);
            Assert.IsTrue(isCompleted2);
            Assert.IsNotNull(active);
            Assert.IsNotNull(completed);
            Assert.AreEqual(0, currentlyActive.Count);
        }

        [TestMethod]
        public void GettingFilteredTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem("A");
            var todoItem2 = new TodoItem("D");
            var todoItem3 = new TodoItem("G");
            var todoItem4 = new TodoItem("P");
            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);
            List<TodoItem> filtered = repository.GetFiltered( t => t.Text.CompareTo("f") == -1);
            Assert.AreEqual(filtered.Count, 2);
        }

        [TestMethod]
        public void UpdatingAndRemovingTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem("A");
            var todoItem2 = new TodoItem("D");
            repository.Add(todoItem1);
            Assert.IsTrue(repository.Remove(todoItem1.Id));
            repository.Update(todoItem1);
            Assert.IsFalse(repository.Remove(todoItem2.Id));
            repository.Add(todoItem2);
            repository.MarkAsCompleted(todoItem2.Id);
            repository.Update(todoItem2);
            Assert.AreEqual(repository.GetAll().Count, 2);
        }
    }
}
