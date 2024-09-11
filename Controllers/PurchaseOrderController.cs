using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PO_Assignment_Project.Data;
using PO_Assignment_Project.Models;

namespace PO_Assignment_Project.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var purchaseOrders = _context.PurchaseOrders
        //        .Include(p => p.Vendor)
        //        .Include(p => p.PurchaseOrderDetails)
        //        .ToListAsync();

        //    return View(await purchaseOrders);
        //}
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .ToListAsync();
            return View(purchaseOrders);
        }

        public IActionResult Create()
        {
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "Name");
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "ShortText");
            var purchaseOrder = new PurchaseOrder
            {
                PurchaseOrderDetails = new List<PurchaseOrderDetails>
        {
            new PurchaseOrderDetails() 
        }
            };
            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "Name", purchaseOrder.VendorID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "ShortText");
            return View(purchaseOrder);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.PurchaseOrderDetails)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "Name", purchaseOrder.VendorID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "ShortText");

            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.ID))
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

            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "Name", purchaseOrder.VendorID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "ShortText");
            return View(purchaseOrder);
        }

        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var purchaseOrder = await _context.PurchaseOrders
        //        .Include(p => p.Vendor)
        //        .Include(p => p.PurchaseOrderDetails)
        //        .ThenInclude(d => d.Material)
        //        .FirstOrDefaultAsync(p => p.ID == id);

        //    if (purchaseOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(purchaseOrder);
        //}

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.PurchaseOrderDetails)
                .ThenInclude(d => d.Material)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }



        private bool PurchaseOrderExists(long id)
        {
            return _context.PurchaseOrders.Any(p => p.ID == id);
        }
    }
}
