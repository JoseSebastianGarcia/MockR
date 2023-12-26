using Microsoft.AspNetCore.Http;
using MockR.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MockR.Services
{
    public class MockRRepository : IMockRRepository
    {
        private readonly CacheDbContext _context;
        public MockRRepository(CacheDbContext context) 
        {
            _context = context;
        }
        public void Create(Page page)
        {
            _context.Pages.Add(page);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Page? page = _context.Pages.Find(id);

            if (page != null)
            {
                _context.Pages.Remove(page);
                _context.SaveChanges();
            }
        }

        public List<Page> GetAll() => _context.Pages.ToList();

        public Page? GetBy(PathString absolutePath, string method)
            => _context.Pages.FirstOrDefault(p=> p.AbsolutePath == absolutePath && p.Method == method);
        

        public void Update(Page page)
        {
            _context.Pages.Update(page);
            _context.SaveChanges();
        }
    }
}
