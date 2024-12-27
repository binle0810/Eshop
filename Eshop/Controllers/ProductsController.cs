using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.Data;
using Eshop.Models;
using Eshop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Eshop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public int PageSize = 6;
        // GET: Products
        public async Task<IActionResult> Index(int PageIndex=1)
        {
            PagingInfo pagingInfo = new PagingInfo
            {
                PageSize = PageSize,
                PageIndex = PageIndex,
                TotalCount = _context.Products.Count()
            };
            if (PageIndex < 1)
            {
                PageIndex = 1; // Đặt về trang đầu tiên
            }
            else if (PageIndex > pagingInfo.TotalPages)
            {
                PageIndex = pagingInfo.TotalPages; // Đặt về trang cuối cùng
            }
            return View(
                new ProductListViewmodel()
                {
                    Products = _context.Products.Skip((PageIndex-1)*PageSize).Take(PageSize)
                    ,
                   PagingInfo = pagingInfo
                });
            ;
        }
       // [HttpPost]
        public async Task<IActionResult> Search(string keywork=null,int PageIndex = 1)
        {
              PagingInfo pagingInfo = new PagingInfo
              {
                  PageSize = PageSize,
                  PageIndex = PageIndex,
                  TotalCount = String.IsNullOrEmpty(keywork)
                  ? _context.Products.Count()
                  : _context.Products.Where(p => p.ProductName.Contains(keywork)).Count()
        };
            var product = String.IsNullOrEmpty(keywork)?_context.Products:_context.Products.Where(p=>p.ProductName.Contains( keywork));
              if (PageIndex < 1)
              {
                  PageIndex = 1; // Đặt về trang đầu tiên
              }
              else if (PageIndex > pagingInfo.TotalPages)
              {
                  PageIndex = pagingInfo.TotalPages; // Đặt về trang cuối cùng
              }
              return View("Index",
                  new ProductListViewmodel()
                  {
                      Products = product.Skip((PageIndex - 1) * PageSize).Take(PageSize)
                      ,
                      PagingInfo = pagingInfo
                      ,
                      keywork=keywork
                  });
              ;
          
        }
        public async Task<IActionResult> ProductByCategory(int categoryid)
        {
            var applicationDbContext = _context.Products.Where(p=>p.CatogeryId==categoryid).Include(p => p.Category).Include(p => p.Color).Include(p => p.Size);
            return View("index",await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public class PriceRange
        {
            public int Min { get; set; }
            public int Max { get;set ;}
        }
        [HttpPost]
        public IActionResult GetFilteredProducts([FromBody] FilterData filter)
        {
            // Lấy danh sách sản phẩm ban đầu
         var filteredProducts = _context.Products.ToList();

            // Kiểm tra nếu bộ lọc PriceRanges không rỗng và hợp lệ
            if (filter.PriceRanges != null && filter.PriceRanges.Count > 0 && !filter.PriceRanges.Contains("all"))
            {
                List<PriceRange> priceRanges = new List<PriceRange>();

                foreach (var range in filter.PriceRanges)
                {
                    var values = range.Split("-").ToArray();
                    PriceRange priceRange = new PriceRange
                    {
                       Min = Int16.Parse(values[0]),
                        Max = Int16.Parse(values[1])
                    };
                    priceRanges.Add(priceRange);
                }

                // Lọc các sản phẩm dựa trên khoảng giá
                filteredProducts = filteredProducts.Where(p =>
                    priceRanges.Any(r => p.ProductPrice >= r.Min && p.ProductPrice <= r.Max)).ToList();
            }

            return PartialView("_ReturnProducts", filteredProducts);
           
        }

        // GET: Products/Create
        [Authorize]

        public IActionResult Create()
        {
            ViewData["CatogeryId"] = new SelectList(_context.Categorys, "CategoryId", "CategoryName");
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDecription,CatogeryId,ProductPrice,ProductDiscount,ProductPhoto,SizeId,ColorId,IsTrend,IsNew")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatogeryId"] = new SelectList(_context.Categorys, "CategoryId", "CategoryName", product.CatogeryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", product.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", product.SizeId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CatogeryId"] = new SelectList(_context.Categorys, "CategoryId", "CategoryName", product.CatogeryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", product.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", product.SizeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDecription,CatogeryId,ProductPrice,ProductDiscount,ProductPhoto,SizeId,ColorId,IsTrend,IsNew")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CatogeryId"] = new SelectList(_context.Categorys, "CategoryId", "CategoryName", product.CatogeryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", product.ColorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", product.SizeId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
