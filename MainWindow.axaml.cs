
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab02_XAML_Layouts_and_Events;

public partial class MainWindow : Window
{
    private readonly List<string> history = new();

    public MainWindow()
    {
        InitializeComponent();

        ExitMenuItem.Click += Exit_Click;
        AboutMenuItem.Click += About_Click;
        CalculateButton.Click += Calculate_Click;
        RegisterButton.Click += Register_Click;
        ShowCategoriesBox.IsCheckedChanged += ShowCategories_Changed;

        CategoryInfo.IsVisible = ShowCategoriesBox.IsChecked == true;
    }

    private void Calculate_Click(object? sender, RoutedEventArgs e)
    {
        bool weightOk = double.TryParse(
            WeightBox.Text?.Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out double weight);

        bool heightOk = double.TryParse(
            HeightBox.Text?.Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out double height);

        if (!weightOk || !heightOk || weight <= 0 || height <= 0)
        {
            BmiResult.Text = "--";
            CategoryResult.Text = "Enter valid positive values.";
            StatusText.Text = "Invalid input";
            return;
        }

        // Convert weight to kilograms.
        if (WeightUnitBox.SelectedIndex == 1)
            weight *= 0.4536;

        // Convert height to metres.
        if (HeightUnitBox.SelectedIndex == 0)
            height /= 100;
        else
            height *= 0.0254;

        double bmi = weight / (height * height);

        if (bmi < 10 || bmi > 100)
        {
            BmiResult.Text = "--";
            CategoryResult.Text = "Please check your measurements.";
            StatusText.Text = "Measurements out of range";
            return;
        }

        string category;

        if (bmi < 18.5)
            category = "Underweight";
        else if (bmi < 25)
            category = "Normal weight";
        else if (bmi < 30)
            category = "Overweight";
        else
            category = "Obese";

        BmiResult.Text = bmi.ToString("F2", CultureInfo.InvariantCulture);
        CategoryResult.Text = category;
        StatusText.Text = "Calculation completed";


    }

    private void ShowCategories_Changed(
        object? sender, RoutedEventArgs e)
    {
        CategoryInfo.IsVisible = ShowCategoriesBox.IsChecked == true;
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void About_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "About",
            Width = 300,
            Height = 150,
            Content = new TextBlock
            {
                Text = "BMI Calculator\nList 2 - XAML Layouts and Events",
                Margin = new Thickness(20),
                TextWrapping = Avalonia.Media.TextWrapping.Wrap
            },
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        await dialog.ShowDialog(this);
    }

    private async void Register_Click(object? sender, RoutedEventArgs e)
    {
        var window = new RegisterWindow();
        await window.ShowDialog(this);
    }
}
