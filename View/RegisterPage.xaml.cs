using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernLoginApp;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}

public class RegisterViewModel : INotifyPropertyChanged
{
    private string _fullName;
    private string _email;
    private string _password;
    private string _confirmPassword;
    private bool _termsAccepted;
    private bool _isBusy;

    public string FullName { get => _fullName; set => SetProperty(ref _fullName, value); }
    public string Email { get => _email; set => SetProperty(ref _email, value); }
    public string Password { get => _password; set => SetProperty(ref _password, value); }
    public string ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }
    public bool TermsAccepted { get => _termsAccepted; set => SetProperty(ref _termsAccepted, value); }
    public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

    public Command RegisterCommand => new(async () =>
    {
        if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) || Password != ConfirmPassword || !TermsAccepted)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields correctly.", "OK");
            return;
        }

        IsBusy = true;
        await Task.Delay(2000); // simulate register
        IsBusy = false;
        await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
    });

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
