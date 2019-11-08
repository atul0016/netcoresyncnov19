using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAppWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAppWeb.Services
{
    public class CategoryService : IService<Category, int>
    {
        private readonly SyncDbContext ctx;
        /// <summary>
        /// Injected the DbContext in Srevice class
        /// </summary>
        /// <param name="ctx"></param>
        public CategoryService(SyncDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            try
            {
                var res = await ctx.Categories.AddAsync(entity);
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
                var res = await ctx.Categories.FindAsync(id);
                if (res == null)
                {
                    throw new Exception($"Record eith id as {id} for Delete not found");
                }
                ctx.Categories.Remove(res);
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            try
            {
                var res = await ctx.Categories.FindAsync(id);
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

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            try
            {
                var res = await ctx.Categories.FindAsync(id);
                if (res == null)
                {
                    throw new Exception($"Record with id as {id} for update not found");
                }
                if (id != entity.CategoryRowId)
                {
                    throw new Exception($"Record search criteria does not match");
                }
                res.CategoryId = entity.CategoryId;
                res.CategoryName = entity.CategoryName;
                res.BasePrice = entity.BasePrice;
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
