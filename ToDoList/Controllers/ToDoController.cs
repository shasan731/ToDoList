using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext context;

        public ToDoController(ToDoDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<DoList> items = from i in context.DoList orderby i.Id select i;

            List<DoList> toDoLists = await items.ToListAsync();

            return View(toDoLists);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoList item)
        {
            if (ModelState.IsValid)
            {
                item.Date = DateTime.Now;
                context.Add(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The task has been added.";
                return RedirectToAction("Index");

            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            DoList item = await context.DoList.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The task has been updated.";
                return RedirectToAction("Index");

            }

            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            DoList item = await context.DoList.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The task was not found!";
            }
            else
            {
                context.DoList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The task has been deleted.";
            }

            return RedirectToAction("Index");
        }

    }
}
