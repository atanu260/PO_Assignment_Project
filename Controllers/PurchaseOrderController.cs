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
                // Automatically generate a 3-4 digit order number
                Random rnd = new Random();
                string newOrderNumber;
                bool isUnique = false;

                
                do
                {
                    newOrderNumber = rnd.Next(100, 10000).ToString();
                    isUnique = !_context.PurchaseOrders.Any(po => po.OrderNumber == newOrderNumber);
                } while (!isUnique);

                
                purchaseOrder.OrderNumber = newOrderNumber;

           
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "Name", purchaseOrder.VendorID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "ShortText");
            return View(purchaseOrder);
        }





        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, PurchaseOrder purchaseOrder)
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

        

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.PurchaseOrderDetails)
                .ThenInclude(d => d.Material)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (purchaseOrder is null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderDetails)
                .FirstOrDefaultAsync(po => po.ID == id);

            if (purchaseOrder != null)
            {
                // Remove associated purchase order details first
                _context.PurchaseOrderDetails.RemoveRange(purchaseOrder.PurchaseOrderDetails);

                // Remove the purchase order itself
                _context.PurchaseOrders.Remove(purchaseOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(p => p.ID == id);
        }
    }
}
