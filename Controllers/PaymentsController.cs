using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PO_Assignment_Project.Data;
using PO_Assignment_Project.Models;
using System.Data.Common;

namespace PO_Assignment_Project.Controllers
{
    public class PaymentsController : Controller
    {   
        private readonly ApplicationDbContext _context;
        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payment.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
            
        }

        public IActionResult Edit(int id)
        {
            if (id is 0 or <0)
            {
                throw new Exception("Id cannot be 0 or negative");
            }

            var payment = _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id  != payment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) { 
            try
            {
                _context.Update(payment);
                    await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.ToString());
            }
                return RedirectToAction(nameof(Index));
            }
            return View(payment);

        }
        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var payment = _context.Payment.FirstOrDefaultAsync(x => x.Id == id);
            if (payment is null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }
    }
}
