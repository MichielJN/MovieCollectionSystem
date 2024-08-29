using MovieCollectionSystem.MVVM.models;

namespace MovieCollectionSystem.views;

public partial class UserHome : ContentPage
{
	private User loggedInUser;
	public UserHome(User user)
	{
		this.loggedInUser = App.UserRepo.GetEntityWithChildren(user.Id);
		InitializeComponent();
        this.ownedmovie_carousel.ItemsSource = loggedInUser.OwnedMovies.ToList().FindAll(x => x.InWishList == false);
        if(loggedInUser.OwnedMovies.ToList().FindAll(x => x.InWishList == true).Count() != 0)
        {
            this.moviewishlist_carousel.ItemsSource = loggedInUser.OwnedMovies.ToList().FindAll(x => x.InWishList == true);
        }

    }

    private async void addmovie_button_Clicked(object sender, EventArgs e)
    {
		await Application.Current.MainPage.Navigation.PushAsync(new AddMovie(loggedInUser));
    }

    private async void addtofavourites_button_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void addtoownedmovies_button_Clicked(object sender, EventArgs e)
    {
        Movie movie = App.MovieRepo.GetEntityWithChildren(((int)((Button)sender).BindingContext));
        movie.InWishList = false;
        App.MovieRepo.SaveEntityWithChildren(movie);
        await Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(loggedInUser));
        //(App.MovieRepo.GetEntityWithChildren(((int)((Button)sender).BindingContext)).InWishList = false);
        
    }

    private void editmovie_button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new AddMovie(loggedInUser, App.MovieRepo.GetEntityWithChildren(((int)((Button)sender).BindingContext))));
    }

    private async void deletemovie_button_Clicked(object sender, EventArgs e)
    {
        if(await DisplayAlert("Bevestig verwijdering", "Weet je zeker dat je " + App.MovieRepo.GetEntity(((int)((Button)sender).BindingContext)).Name + " Wilt verwijderen?", "Ja", "nee") == true)
        {
            App.MovieRepo.DeleteEntity(App.MovieRepo.GetEntity(((int)((Button)sender).BindingContext)));
            await Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(loggedInUser));
        }
        
    }
}