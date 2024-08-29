using MovieCollectionSystem.views;
using MovieCollectionSystem.MVVM.models;

namespace MovieCollectionSystem
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            this.name_picker.ItemsSource = App.UserRepo.GetEntitiesWithChildren();
            ToggleShake();
        }


        private async void login_button_Clicked(object sender, EventArgs e)
        {
            if(name_picker.SelectedIndex != -1)
            {
                User user = name_picker.SelectedItem as User;

                    if(user.Password == password_entry.Text)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
                    }
                    else
                    {
                        password_entry.Text = "";
                        password_entry.Placeholder = "Wachtwoord is niet correct";
                    }
            }
            else
            {
                password_entry.Text = "";
                password_entry.Placeholder = "Selecteer een gebruiker";
            }

        }

        private async void createuser_button_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateUser());
        }

        private void ToggleShake()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
                else
                {
                    // Turn off accelerometer
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                }
            }
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Update UI Label with a "shaked detected" notice, in a randomized color
            if (name_picker.SelectedIndex != -1)
            {
                User user = name_picker.SelectedItem as User;

                if (user.Password == password_entry.Text)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
                }
                else
                {
                    password_entry.Text = "";
                    password_entry.Placeholder = "Wachtwoord is niet correct";
                }
            }
            else
            {
                password_entry.Text = "";
                password_entry.Placeholder = "Selecteer een gebruiker";
            }
        }

    }

}
