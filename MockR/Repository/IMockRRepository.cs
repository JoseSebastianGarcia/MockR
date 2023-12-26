using Microsoft.AspNetCore.Http;
using MockR.Entities;
using System.Collections.Generic;

namespace MockR.Services
{
    public interface IMockRRepository
    {
        void Create(Page page);
        void Update(Page page);
        void Delete(int id);
        List<Page> GetAll();
        Page? GetBy(PathString absolutePath, string method);
    }
}
