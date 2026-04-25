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
    public class UserSkillsController : Controller
    {
        private readonly AppDbContext _context;

        public UserSkillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserSkills
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserSkills.Include(u => u.Skill).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.Skill)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // GET: UserSkills/Create
        public IActionResult Create()
        {
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillDesc");
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserEmail");
            return View();
        }

        // POST: UserSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,SkillID")] UserSkill userSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillDesc", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserEmail", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills.FindAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillDesc", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserEmail", userSkill.UserID);
            return View(userSkill);
        }

        // POST: UserSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,SkillID")] UserSkill userSkill)
        {
            if (id != userSkill.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSkillExists(userSkill.UserID))
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
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillDesc", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserEmail", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.Skill)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // POST: UserSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSkill = await _context.UserSkills.FindAsync(id);
            if (userSkill != null)
            {
                _context.UserSkills.Remove(userSkill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSkillExists(int id)
        {
            return _context.UserSkills.Any(e => e.UserID == id);
        }
    }
}
