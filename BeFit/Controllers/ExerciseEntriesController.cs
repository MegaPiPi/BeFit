using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Controllers
{
    [Authorize]
    public class ExerciseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExerciseEntriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ExerciseEntries
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.TrainingSession.UserId == userId).OrderByDescending(e => e.TrainingSession.StartTime).ToListAsync();

            return View(entry);
        }

        // GET: ExerciseEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries
                .Include(e => e.TrainingSession)
                .Include(e => e.ExerciseType)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: ExerciseEntries/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);

            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == userId), "Id", "StartTime", "EndTime");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");

            return View(new ExerciseEntryCreateDto());
        }

        // POST: ExerciseEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseEntryCreateDto dto)
        {
            var userId = _userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", dto.ExerciseTypeId);
                ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == userId), "Id", "StartTime", dto.TrainingSessionId);
                return View(dto);
            }

            var session = await _context.TrainingSessions.FirstOrDefaultAsync(s => s.Id == dto.TrainingSessionId && s.UserId == userId);

            if (session == null)
            {
                return BadRequest("Nie masz dostępu do tej sesji");
            }

            var entry = new ExerciseEntry
            {
                ExerciseTypeId = dto.ExerciseTypeId,
                TrainingSessionId = dto.TrainingSessionId,
                Weight = dto.Weight,
                Sets = dto.Sets,
                Repetitions = dto.Repetitions,
            };

            _context.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ExerciseEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries.FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (entry == null)
            {
                return NotFound();
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", entry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == userId), "Id", "StartTime", entry.TrainingSessionId);

            return View(entry);
        }

        // POST: ExerciseEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExerciseEntry exerciseEntry)
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries.Include(e => e.TrainingSession).FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (entry == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
                ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == userId), "Id", "StartTime", exerciseEntry.TrainingSessionId);
                return View(entry);
            }

            var session = await _context.TrainingSessions.FirstOrDefaultAsync(s => s.Id == exerciseEntry.TrainingSessionId && s.UserId == userId);

            if (session == null)
            {
                return BadRequest("Nie masz dostępu do tej sesji");
            }

            entry.TrainingSessionId = exerciseEntry.TrainingSessionId;
            entry.ExerciseTypeId = exerciseEntry.ExerciseTypeId;
            entry.Weight = exerciseEntry.Weight;
            entry.Sets = exerciseEntry.Sets;
            entry.Repetitions = exerciseEntry.Repetitions;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ExerciseEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: ExerciseEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            var entry = await _context.ExerciseEntries.Include(e => e.TrainingSession).FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (entry == null)
            {
                return NotFound();
            }

            _context.ExerciseEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseEntryExists(int id)
        {
            return _context.ExerciseEntries.Any(e => e.Id == id);
        }
    }
}