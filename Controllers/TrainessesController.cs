using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstCoreMVC.Models;
using NToastNotify;


namespace MyFirstCoreMVC.Controllers
{
    public class TrainessesController : Controller
    { 
        private readonly ILogger<TrainessesController> _logger;
        private readonly IToastNotification _toastNotification;
        private readonly TE_Core_MVCContext _context;
        public TrainessesController(ILogger<TrainessesController> logger, IToastNotification toastNotification, TE_Core_MVCContext context)
        {
            _logger = logger;
            _toastNotification = toastNotification;
            _context = context;
        }

    // GET: Trainees
    public async Task<IActionResult> Index()
    {
        return View(await _context.Trainesses.ToListAsync());
    }

    // GET: Trainees/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Trainesses == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainesses
            .FirstOrDefaultAsync(m => m.Tid == id);
        if (trainee == null)
        {
            return NotFound();
        }

        return View(trainee);
    }

    // GET: Trainees/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Trainees/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Tid,Tname,Tage")] Trainess trainee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(trainee);
            await _context.SaveChangesAsync();
            //toastNotification in green color - > SUCCESS
            _toastNotification.AddSuccessToastMessage("Employee created successfully");
            return RedirectToAction(nameof(Index));
        }
        return View(trainee);
    }

    // GET: Trainees/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Trainesses == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainesses.FindAsync(id);
        if (trainee == null)
        {
            return NotFound();
        }
        return View(trainee);
    }

    // POST: Trainees/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Tid,Tname,Tage")] Trainess trainee)
    {
        if (id != trainee.Tid)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(trainee);
                await _context.SaveChangesAsync();
                //toastNotification in yellow color - > WARNING
                _toastNotification.AddWarningToastMessage("Employee updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(trainee.Tid))
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
        return View(trainee);
    }

    // GET: Trainees/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Trainesses == null)
        {
            return NotFound();
        }

        var trainee = await _context.Trainesses
            .FirstOrDefaultAsync(m => m.Tid == id);
        if (trainee == null)
        {
            return NotFound();
        }

        return View(trainee);
    }

    // POST: Trainees/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Trainesses == null)
        {
            return Problem("Entity set 'TE_Core_MVCContext.Trainees'  is null.");
        }
        var trainee = await _context.Trainesses.FindAsync(id);
        if (trainee != null)
        {
            _context.Trainesses.Remove(trainee);
        }

        await _context.SaveChangesAsync();
        //toastNotification in red color - > Error
        _toastNotification.AddErrorToastMessage("Employee deleted successfully");
        return RedirectToAction(nameof(Index));
    }

    private bool TraineeExists(int id)
    {
        return _context.Trainesses.Any(e => e.Tid == id);
    }
    }
}
