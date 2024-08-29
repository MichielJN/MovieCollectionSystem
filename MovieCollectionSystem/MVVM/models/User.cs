using MovieCollectionSystem.abstractions;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionSystem.MVVM.models
{
    public class User : TableData
    {
        public string Password { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Movie>? OwnedMovies { get; set; } = new List<Movie>();

        public User()
        {

        }
        public User(string name, string password)
        {
            this.Id = 0;
            this.Name = name;
            this.Password = password;
        }

    }
}
