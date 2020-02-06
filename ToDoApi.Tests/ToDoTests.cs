using System;
using Xunit;
using ToDoApi.Controllers;
using ToDoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ToDoApi.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void TestAdd()
        {
            using( var setup = new SetupToDoController())
            {
                var controller = setup.Controller;
                var context = setup.Context;
                var item = new ToDoItem();
                item.Name = "Test Item";
                controller.Create(item);
                var dbResult = context.ToDoItems.Where( a=>a.Name == "Test Item");
                Assert.True( dbResult.Count() == 1, "Should be only 1");
            }
        }
        // Just one test above for coding sample ... 
    }
}
