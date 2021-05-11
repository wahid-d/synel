using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace mvc.Utils.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<List<string>> ReadLinesAsList(this IFormFile file)
        {
            var list = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    list.Add(await reader.ReadLineAsync());
            }
            return list;
        }
    }
}
