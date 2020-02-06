using System;
using Xunit;
using ToDoApi.Controllers;
using ToDoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Tests
{
    public class SetupToDoController : IDisposable
    {
        public ToDoContext Context { get; set;}
        public ToDoController Controller { get; set;}

        public SetupToDoController() {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();
            optionsBuilder.UseInMemoryDatabase( databaseName: "todo");
            Context = new ToDoContext( optionsBuilder.Options);
            var repo = new ToDoRepository( Context);
            Controller = new ToDoController(repo);
        }

        public void Dispose() {
            Context.Dispose();
        }
    }
}