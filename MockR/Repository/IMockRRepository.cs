using Microsoft.AspNetCore.Http;
using MockR.Entities;
using System;
using System.Collections.Generic;

namespace MockR.Services
{
    public interface IMockRRepository
    {
        void Create(Page page);
        void Delete(Guid id);
        List<Page> GetAll();
        Page? GetBy(PathString absolutePath, string method);
    }
}
