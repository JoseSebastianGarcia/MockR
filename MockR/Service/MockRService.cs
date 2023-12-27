using Microsoft.AspNetCore.Http;
using MockR.Dtos;
using MockR.Entities;
using MockR.Request;
using MockR.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockR.Service
{
    public class MockRService : IMockRService
    {
        private readonly IMockRRepository _repository;

        public MockRService(IMockRRepository repository) 
        {
            _repository = repository;
        }

        public void Create(CreateRequest page)
        {
            _repository.Create(new Page()
            {
                Id = Guid.NewGuid(),
                Method = page.Method,
                AbsolutePath = page.AbsolutePath,
                Body = page.Body,
            });
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<PageDto> GetAll()
        {
            var pages = _repository.GetAll();

            return pages.Select(x => new PageDto() 
            {  
                Id = x.Id,
                Body = x.Body ,
                Method = x.Method,
                AbsolutePath = x.AbsolutePath
            }).ToList();
        }

        public PageDto? GetBy(PathString absolutePath, string method)
        {
            Page? page = _repository.GetBy(absolutePath, method);

            PageDto? dto = null;

            if (page != null)
            {
                dto = new PageDto();

                dto.Id = page.Id;
                dto.AbsolutePath = absolutePath;
                dto.Method = method;
                dto.Body = page.Body;
            }

            return dto;
        }

    }
}
