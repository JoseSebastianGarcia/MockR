using Microsoft.AspNetCore.Http;
using MockR.Dtos;
using MockR.Request;
using System.Collections.Generic;

namespace MockR.Service
{
    public interface IMockRService
    {
        void Create(CreateRequest page);
        void Update(UpdateRequest page);
        void Delete(int id);
        List<PageDto> GetAll();
        PageDto? GetBy(PathString absolutePath, string method);
    }
}
