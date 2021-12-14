using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DollarShop.DB;
using DollarShop.Models;

namespace DollarShop.Controllers
{
    public class TestController : Controller
    {
        private readonly DollarShopContext _context;

        public TestController(DollarShopContext context)
        {
            _context = context;
        }

        // GET: Test
        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModelUpdated = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productsModelUpdated == null)
            {
                return NotFound();
            }

            return View(productsModelUpdated);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Catagory,Image,Price,ProductID")] ProductsModelUpdated productsModelUpdated)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productsModelUpdated);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productsModelUpdated);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModelUpdated = await _context.Products.FindAsync(id);
            if (productsModelUpdated == null)
            {
                return NotFound();
            }
            return View(productsModelUpdated);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Catagory,Image,Price,ProductID")] ProductsModelUpdated productsModelUpdated)
        {
            if (id != productsModelUpdated.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsModelUpdated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsModelUpdatedExists(productsModelUpdated.ProductID))
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
            return View(productsModelUpdated);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModelUpdated = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productsModelUpdated == null)
            {
                return NotFound();
            }

            return View(productsModelUpdated);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsModelUpdated = await _context.Products.FindAsync(id);
            _context.Products.Remove(productsModelUpdated);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsModelUpdatedExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
