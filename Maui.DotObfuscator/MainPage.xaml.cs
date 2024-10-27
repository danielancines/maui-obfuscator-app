using Maui.DotObfuscator.Commons;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Maui.DotObfuscatorApp;

public partial class MainPage : ContentPage
{
    const string Url = "http://google.com";
    private readonly IHttpClientFactory _httpClientFactory;

    public ObservableCollection<Person> People { get; } = new();
    public MainPage(IHttpClientFactory httpClientFactory)
    {
        InitializeComponent();
        this.Loaded += MainPage_Loaded;
        this._httpClientFactory = httpClientFactory;
    }

    private void MainPage_Loaded(object? sender, EventArgs e)
    {
        this.PersonList.ItemsSource = People;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        string fileHash;
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead("D:\\code\\Maui.DotObfuscatorApp\\Maui.DotObfuscator\\Obfuscar\\obfuscate.xml"))
            {
                var hash = md5.ComputeHash(stream);
                fileHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        var httpClient = this._httpClientFactory.CreateClient(HttpClientTypes.Regular);
        await httpClient.GetAsync("https://efs.btgpactualdigital.com/trader");

        var person = new Person()
        {
            Name = fileHash,
            Age = Random.Shared.Next(0, 100)
        };

        this.People.Add(person);
    }


}
