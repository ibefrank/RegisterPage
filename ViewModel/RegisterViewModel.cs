using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace YourApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string fullName;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            ErrorMessage = string.Empty;

            if (!ValidateInputs())
                return;

            IsBusy = true;

            try
            {
                // Simulate a registration process (e.g., API call)
                await Task.Delay(2000);

                // TODO: Replace with real registration logic
                await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");

                // Optional: Navigate to login or home page
                // await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Registration failed: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                return false;
            }

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Invalid email format.";
                return false;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters.";
                return false;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return false;
            }

            return true;
        }
    }
}
