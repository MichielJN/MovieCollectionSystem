using MovieCollectionSystem.MVVM.models;
using MovieCollectionSystem.Repositories;

namespace MovieCollectionSystem
{
    public partial class App : Application
    {
        public static BaseRepository<User>? UserRepo { get; private set; }
        public static BaseRepository<Movie>? MovieRepo { get; private set; }
        public static BaseRepository<Genre>? GenreRepo { get; private set; }
        public App(BaseRepository<User>? userRepo,
            BaseRepository<Movie> movieRepo,
            BaseRepository<Genre> genreRepo)
        {
            InitializeComponent();

            UserRepo = userRepo;
            MovieRepo = movieRepo;
            GenreRepo = genreRepo;

            MainPage = new AppShell();
        }
    }
}
