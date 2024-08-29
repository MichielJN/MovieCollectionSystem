using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionSystem.helpers
{
    public class Movie
    {
        public static string GenerateURLName(string name)
        {
            return string.Format(Constants.MOVIE_BY_NAME, name);
        }

        public static string GenerateURLRandom()
        {
            return Constants.RANDOM_MOVIE;
        }
    }
}
