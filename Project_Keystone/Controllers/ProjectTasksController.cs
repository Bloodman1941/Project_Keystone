using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Data;
using Project_Keystone.Models;

namespace Project_Keystone.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProjectTasks.Include(p => p.Priority).Include(p => p.Project).Include(p => p.Status);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.Priority)
                .Include(p => p.Project)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["TaskPriority"] = new SelectList(_context.Priorities, "PriorityID", "PriorityColor");
            ViewData["TaskProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectDesc");
            ViewData["TaskStatus"] = new SelectList(_context.Statuses, "StatusID", "StatusColor");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,TaskName,TaskDesc,TaskPriority,TaskStatus,TaskTimeStamp,TaskDueDate,TaskProjectID")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskPriority"] = new SelectList(_context.Priorities, "PriorityID", "PriorityColor", projectTask.TaskPriority);
            ViewData["TaskProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectDesc", projectTask.TaskProjectID);
            ViewData["TaskStatus"] = new SelectList(_context.Statuses, "StatusID", "StatusColor", projectTask.TaskStatus);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["TaskPriority"] = new SelectList(_context.Priorities, "PriorityID", "PriorityColor", projectTask.TaskPriority);
            ViewData["TaskProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectDesc", projectTask.TaskProjectID);
            ViewData["TaskStatus"] = new SelectList(_context.Statuses, "StatusID", "StatusColor", projectTask.TaskStatus);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,TaskName,TaskDesc,TaskPriority,TaskStatus,TaskTimeStamp,TaskDueDate,TaskProjectID")] ProjectTask projectTask)
        {
            if (id != projectTask.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.TaskID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskPriority"] = new SelectList(_context.Priorities, "PriorityID", "PriorityColor", projectTask.TaskPriority);
            ViewData["TaskProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectDesc", projectTask.TaskProjectID);
            ViewData["TaskStatus"] = new SelectList(_context.Statuses, "StatusID", "StatusColor", projectTask.TaskStatus);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.Priority)
                .Include(p => p.Project)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask != null)
            {
                _context.ProjectTasks.Remove(projectTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.TaskID == id);
        }
    }
}
