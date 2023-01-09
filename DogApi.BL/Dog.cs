using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogApi.BL
{
    public class Dog
    {
        public static string breed(string message)
        {
            var breedList = message.Split('/')[4].Split("-").Reverse();
            var breed = "";
            foreach (var item in breedList)
            {
                breed += char.ToUpper(item[0]) + item.Substring(1) + " ";
            }
            
            return breed;
        }
    }
}
