﻿using EasyCommerce.Data;
using EasyCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Interfaces
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    { 
        private readonly BD_Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BD_Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();

            return list;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
