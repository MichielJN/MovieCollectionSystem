using MovieCollectionSystem.MVVM.models;

namespace MovieCollectionSystem.views;

public partial class CreateUser : ContentPage
{
	public CreateUser()
	{
		InitializeComponent();
	}

    private async void create_button_Clicked(object sender, EventArgs e)
    {
		if(name_entry.Text != "" && password_entry.Text != "")
		{
            App.UserRepo.SaveEntity(new User(name_entry.Text, password_entry.Text));
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
        }
		else
		{
			name_entry.Placeholder = "Voer alle velden in";
			password_entry.Placeholder = "Voer alle velden in";
		}
		
    }

    private async void back_button_Clicked(object sender, EventArgs e)
    {
		await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
    }
}