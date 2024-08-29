using MovieCollectionSystem.abstractions;
using SQLiteNetExtensions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace MovieCollectionSystem.MVVM.models
{
    public class Genre : TableData
    {
        [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<Movie>?  Movies { get; set; }

        public Genre()
        {

        }
        public Genre(string name)
        {
            this.Id = 0;
            this.Name = name;
        }
    }
}
