using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCollectionSystem.helpers;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace MovieCollectionSystem.logic
{
    public class MovieLogic
    {
        public async static Task<string> GetMovieByName(string name)
        {
            var url = Movie.GenerateURLName(name);
            string json;
            MVVM.models.Movie _movie = new MVVM.models.Movie();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                json = await response.Content.ReadAsStringAsync();
               // string _name = JObject.Parse(json)["Title"].ToString();
            }
            return json;
        }
    }
}
