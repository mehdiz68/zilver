using System;
using System.Collections.Generic;
using System.Text;
using zilver.domain.Entities;
using zilver.domain.Interfaces;

namespace zilver.data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Product> Products { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new Repository<Product>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }

}
