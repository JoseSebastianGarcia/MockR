using Microsoft.AspNetCore.Http;
using MockR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockR.Services
{
    public class MockRRepository : IMockRRepository
    {
        private readonly MockRDbContext _context;
        public MockRRepository(MockRDbContext context) 
        {
            _context = context;
        }
        public void Create(Page page)
        {
            _context.Create(page);
        }

        public void Delete(Guid id)
        {
            _context.Delete(id);
        }

        public List<Page> GetAll() 
        {
            List<Page> result = _context.Pages.ToList();
            return result;
        }

        public Page? GetBy(PathString absolutePath, string method)
            => _context.Pages.FirstOrDefault(p=> p.AbsolutePath == absolutePath && p.Method == method);

    }
}
