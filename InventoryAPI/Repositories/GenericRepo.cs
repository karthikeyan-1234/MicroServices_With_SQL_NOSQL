﻿using Microsoft.EntityFrameworkCore;

using InventoryAPI.Infrastructure.Contexts;

namespace InventoryAPI.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        IInventoryDbContext db;
        DbSet<T> table;


        public GenericRepo(IInventoryDbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var res = await table.AddAsync(entity);
            return res.Entity;
        }

        public bool Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return table.Where(predicate).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
    }
}
