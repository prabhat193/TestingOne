﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly HeritageWoodsContext _context;

        public ProductCategoriesController(HeritageWoodsContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
        {
            return await _context.ProductCategory.ToListAsync();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _context.ProductCategory.FindAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Cid)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategory.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.Cid }, productCategory);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategory.Remove(productCategory);
            await _context.SaveChangesAsync();

            return productCategory;
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategory.Any(e => e.Cid == id);
        }
    }
}
