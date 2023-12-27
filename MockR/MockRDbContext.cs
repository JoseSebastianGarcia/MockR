using MockR.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
namespace MockR
{
    public class MockRDbContext 
    {
        private const string _FILENAME = "Pages.json";
        private static object _semaphore = new object();
        public List<Page> Pages { get; private set; }


        public MockRDbContext() 
        {
            Pages = ReadPages();
        }

        public void Create(Page page) 
        {
            Pages.Add(page);
            SavePages();
        }
        public void Delete(Guid guid)
        {
            Page? page = Pages.Find(x => x.Id == guid);

            if (page != null) 
            {
                Pages.Remove(page);
                SavePages();
            }
            
        }


        private void SavePages() 
        {
            lock (_semaphore)
            {
                using (StreamWriter writer = new StreamWriter(_FILENAME))
                {
                    writer.Write(JsonSerializer.Serialize(Pages));
                }
            }
        }

        private List<Page> ReadPages() 
        {
            List<Page> pages = new List<Page>();
            
            lock(_semaphore) 
            {
                if (File.Exists(_FILENAME))
                {
                    using (StreamReader reader = new StreamReader(_FILENAME))
                    {
                        pages = JsonSerializer.Deserialize<List<Page>>(reader.ReadToEnd())!;
                    }
                }
            }

            return pages;
        }
    }


}
