using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoDbContext : DbContext
    {
        internal object toDoList;

        public ToDoDbContext(DbContextOptions<ToDoDbContext>options):base(options)
            {

            }

        public DbSet<DoList> DoList { get; set; }
        public object List { get; internal set; }
    }
}
