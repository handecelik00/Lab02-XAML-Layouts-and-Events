
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Lab02_XAML_Layouts_and_Events;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }

    private void Register_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameBox.Text) ||
            string.IsNullOrWhiteSpace(EmailBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordBox.Text))
        {
            RegisterMessage.Text = "Please fill in all fields.";
            return;
        }

        RegisterMessage.Text = "Registration form completed.";
    }
}
