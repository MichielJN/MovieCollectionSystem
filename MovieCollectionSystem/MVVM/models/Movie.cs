using MovieCollectionSystem.abstractions;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionSystem.MVVM.models
{
    public class Movie : TableData
    {
        public string? Director { get; set; }
        public string? Writer { get; set; }
        public string? Actors { get; set; }
        public string? Description { get; set; }
        [ForeignKey(typeof(Genre))]
        public int? GenreId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Genre? Genre { get; set; }
        public int? LengthHours { get; set; } //The amount of full hours of the movie duration
        public int? LenghtMinutes { get; set; } //The amount of minutes leftover
        public string? MovieLength { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Language { get; set; }
        public string? CountryMadeIn { get; set; }
        [ForeignKey(typeof (User))]
        public int UserId { get; set; }
        [ManyToOne(CascadeOperations =CascadeOperation.All)]
        public User User { get; set; }
        public bool? InWishList { get; set; } = false;
        public bool? IsFavourite { get; set; } = false;
        public string? ImagePath { get; set; } = "dotnet_bot.png";
        
        public Movie()
        {

        }
        public Movie(string name, string director, string writer, string actors, string description, int lengthHours, int lengthMinutes, DateTime releaseDate, string language, string countryMadeIn, User user)//manually add release date and genre
        {
            this.Id = 0;
            this.Name = name;
            this.Director = director;
            this.Writer = writer;
            this.Actors = actors;
            this.Description = description;
            this.LengthHours = lengthHours;
            this.LenghtMinutes = lengthMinutes;
            this.ReleaseDate = releaseDate;
            this.Language = language;
            this.CountryMadeIn = countryMadeIn;
            this.User = user;
            this.UserId = user.Id;
            this.MovieLength = GetMovieLength();
        }

        public string GetMovieLength()
        {
            return this.LengthHours.ToString() + " uur " + this.LenghtMinutes.ToString() + " Minuten";
        }
    }
}
