using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace DogApi.BL
{
    public class Api
    {
        public static async Task<string> CallApi(string url)
        {
            using HttpClient client = new();

            static async Task<string> ProcessRepositoriesAsync(HttpClient client, string url)
            {
                var json = await client.GetStringAsync(url);

                return json;
            }

            return await ProcessRepositoriesAsync(client, url);
        }

        public static async Task<List<string>> GetBreedList()
        {
            var breedList = new List<string>();
            var json = await CallApi("https://dog.ceo/api/breeds/list/all");
            dynamic data = JObject.Parse(json);

            foreach (var breed in data.message)
            {
                if (breed.Value.Count == 0)
                {
                    breedList.Add(breed.Name);
                }
                else
                {
                    foreach (var subBreed in breed.Value)
                    {
                        breedList.Add($"{breed.Name} - {subBreed}");
                    }
                }
            }

            return breedList;
        }

    }
}
