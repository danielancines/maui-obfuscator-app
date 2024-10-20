using Maui.DotObfuscatorCommons;
using System.Collections.ObjectModel;

namespace Maui.DotObfuscatorApp;

public partial class MainPage : ContentPage
{
    const string Url = "http://google.com";
    public ObservableCollection<Person> People { get; } = new();
    public MainPage()
    {
        InitializeComponent();
        this.Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object? sender, EventArgs e)
    {
        this.PersonList.ItemsSource = People;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        var person = new Person()
        {
            Name = "Obfuscator App",
            Age = Random.Shared.Next(0, 100)
        };

        this.People.Add(person);
    }
}
