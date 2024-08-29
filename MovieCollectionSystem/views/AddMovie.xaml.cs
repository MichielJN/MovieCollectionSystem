using MovieCollectionSystem.MVVM.models;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace MovieCollectionSystem.views;

public partial class AddMovie : ContentPage
{
	private User user;
	private string imagePath = "dotnet_bot.png";
    private int edit;

    public AddMovie(User _user)
	{
		InitializeComponent();
		this.genre_picker.ItemsSource = App.GenreRepo.GetEntities();
		this.user = App.UserRepo.GetEntityWithChildren(_user.Id);
	}
    public AddMovie(User _user, Movie movie)//edit movie
    {
        InitializeComponent();
        this.edit = 1;
        this.genre_picker.ItemsSource = App.GenreRepo.GetEntities();
        this.user = App.UserRepo.GetEntityWithChildren(_user.Id);
        name_entry.Text = movie.Name;
        director_entry.Text = movie.Director;
        actor_entry.Text = movie.Actors;
        description_entry.Text = movie.Description;
        writer_entry.Text = movie.Writer;
        country_entry.Text = movie.CountryMadeIn;
        language_entry.Text = movie.Language;
        hour_stepper.Value = ((double)movie.LengthHours);
        minute_slider.Value = ((double)movie.LenghtMinutes);

        
       
        if (movie.Genre != null)
        {
            newgenre_entry.Text = movie.Genre.Name;
            //Genre genre = App.GenreRepo.GetEntities().FirstOrDefault(x => x.Name == movie.Genre.Name);
            //genre_picker.SelectedItem = movie.Genre;
        }
        addtoownedmovies_button.Text = "sla aanpassingen op";
            
        
        
        
        releasedate_datepicker.Date = ((DateTime)movie.ReleaseDate).Date;
    }


    //string name, string director, string Writer, int lengthHours, int lengthMinutes, string language, string countryMadeIn, User user
    private void addtoownedmovies_button_Clicked(object sender, EventArgs e)
    {
        
        if (name_entry.Text != "")
        {
            

            Movie movie = new Movie(name_entry.Text, director_entry.Text, writer_entry.Text, actor_entry.Text, description_entry.Text, (int)hour_stepper.Value, (int)minute_slider.Value, releasedate_datepicker.Date, language_entry.Text, country_entry.Text, user);
            string hallo = "";
            if (newgenre_entry.Text == "")
            {
                if (genre_picker.SelectedIndex != -1)
                {

                    Genre genre = genre_picker.SelectedItem as Genre;
                    movie.Genre = genre;
                    movie.GenreId = genre.Id;
                }
            }
            else
            {
                if (App.GenreRepo.GetEntities().FirstOrDefault(x => x.Name == newgenre_entry.Text) != null)
                {
                    movie.Genre = App.GenreRepo.GetEntities().FirstOrDefault(x => x.Name == newgenre_entry.Text);
                }
                else
                {
                    App.GenreRepo.SaveEntity(new Genre(newgenre_entry.Text));
                    movie.Genre = App.GenreRepo.GetEntities().FirstOrDefault(x => x.Name == newgenre_entry.Text);
                    movie.GenreId = movie.Genre.Id;
                }
                
            }
            
                movie.ImagePath = this.imagePath;
                App.MovieRepo.SaveEntity(movie);
            
            
            


            Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
        
    }
		else
		{

		}
    }

    private void addtomoviewishlistbutton_Clicked(object sender, EventArgs e)
    {
        if (name_entry.Text != "")
        {
            if (name_entry.Text != "")
            {
                Movie movie = new Movie(name_entry.Text, director_entry.Text, writer_entry.Text, actor_entry.Text, description_entry.Text, (int)hour_stepper.Value, (int)minute_slider.Value, releasedate_datepicker.Date, language_entry.Text, country_entry.Text, user);
                string hallo = "";
                if (genre_picker.SelectedIndex != -1)
                {
                    Genre genre = genre_picker.SelectedItem as Genre;
                    movie.Genre = genre;
                    movie.GenreId = genre.Id;
                }
                movie.ImagePath = this.imagePath;
                movie.InWishList = true;
                App.MovieRepo.SaveEntity(movie);
                Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));

            }
            else
            {

            }
        }
        else
        {

        }
    }

    private async void picture_button_Clicked(object sender, EventArgs e)
    {
		if (MediaPicker.Default.IsCaptureSupported)
		{
			FileResult image = await MediaPicker.Default.PickPhotoAsync();
			this.imagePath = image.FullPath;

		}
    }

    private async void searchname_button_Clicked(object sender, EventArgs e)
    {
        string jsonResponse = await MovieCollectionSystem.logic.MovieLogic.GetMovieByName(searchname_entry.Text);
        if (JObject.Parse(jsonResponse)["Response"].ToString() != "False")
        {
            name_entry.Text = JObject.Parse(jsonResponse)["Title"].ToString();
            director_entry.Text = JObject.Parse(jsonResponse)["Director"].ToString();
            actor_entry.Text = JObject.Parse(jsonResponse)["Actors"].ToString();
            description_entry.Text = JObject.Parse(jsonResponse)["Plot"].ToString();
            writer_entry.Text = JObject.Parse(jsonResponse)["Writer"].ToString();
            country_entry.Text = JObject.Parse(jsonResponse)["Country"].ToString();
            language_entry.Text = JObject.Parse(jsonResponse)["Language"].ToString();
            int time = Convert.ToInt32(Regex.Match(JObject.Parse(jsonResponse)["Runtime"].ToString(), @"\d+").Value);
            hour_stepper.Value = ((int)time / 60);
            minute_slider.Value = time % 60;
            newgenre_entry.Text = JObject.Parse(jsonResponse)["Genre"].ToString();
            releasedate_datepicker.Date = ((DateTime)JObject.Parse(jsonResponse)["Released"]);
        }
        else
        {
            searchname_entry.Text = "";
            searchname_entry.Placeholder = "Film bestaat niet";
        }
    }

    private void back_button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
    }
}