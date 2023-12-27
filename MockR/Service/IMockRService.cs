using Microsoft.AspNetCore.Http;
using MockR.Dtos;
using MockR.Request;
using System;
using System.Collections.Generic;

namespace MockR.Service
{
    public interface IMockRService
    {
        void Create(CreateRequest page);
        void Delete(Guid id);
        List<PageDto> GetAll();
        PageDto? GetBy(PathString absolutePath, string method);
    }
}
