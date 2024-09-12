using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PO_Assignment_Project.Data;
using PO_Assignment_Project.Models;

namespace PO_Assignment_Project.Controllers
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext context)
        {
            _context  = context;
        }
        public async Task <IActionResult> Index()
        {
            return View(await _context.Vendors.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(vendor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }
            return View(vendors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendor vendor)
        {
            if (id != vendor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.ID))
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
            return View(vendor);
        }

        //public IActionResult Details()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Details(int id)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.ID == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendor/Delete/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.ID == id);
        }
    }
}
