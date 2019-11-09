using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAppWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAppWeb.Services
{
    public class ProductService : IService<Product, int>
    {
        private readonly SyncDbContext ctx;
        /// <summary>
        /// Injected the DbContext in Srevice class
        /// </summary>
        /// <param name="ctx"></param>
        public ProductService(SyncDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            try
            {
                var res = await ctx.Products.AddAsync(entity);
                await ctx.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var res = await ctx.Products.FindAsync(id);
                if (res == null)
                {
                    throw new Exception($"Record eith id as {id} for Delete not found");
                }
                ctx.Products.Remove(res);
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            try
            {
                var res = await ctx.Products.FindAsync(id);
                if (res == null)
                {
                    throw new Exception($"Record eith id as {id} not found");
                }
               
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            try
            {
                var res = await ctx.Products.FindAsync(id);
                if (res == null)
                {
                    throw new Exception($"Record with id as {id} for update not found");
                }
                if (id != entity.ProductRowId)
                {
                    throw new Exception($"Record search criteria does not match");
                }
                res.ProductId = entity.ProductId;
                res.ProductName = entity.ProductName;
                res.Manufacturer = entity.Manufacturer;
                res.CategoryRowId = entity.CategoryRowId;
                await ctx.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
