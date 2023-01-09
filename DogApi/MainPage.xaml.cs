using Newtonsoft.Json.Linq;
using DogApi.BL;
using Color = Microsoft.Maui.Graphics.Color;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace DogApi;

public partial class MainPage : ContentPage
{
    private const string RandomUrl = "https://dog.ceo/api/breeds/image/random";
    private static string wikiUrl = "https://en.wikipedia.org/wiki/Dog";
    public string CurrentBreed = "All";
    public MainPage()
    {
        InitializeComponent();
        CreateBreedPicker();
        SetRandomDog(RandomUrl);

    #if ANDROID
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetStatusBarColor(Android.Graphics.Color.Black);
    #endif
    }


    private async void SetRandomDog(string randomUrl)
    {
        string json;
        if (CurrentBreed != "All")
        {
            json = await Api.CallApi($"https://dog.ceo/api/breed/{CurrentBreed.ToLower()}/images/random");
        }
        else
        {
            json = await Api.CallApi(RandomUrl);
        }

        dynamic data = JObject.Parse(json);
        string message = data.message;

        Update(message);
    }

    private async void Update(string message)
    {
        string breed = Dog.breed(message);
        
        wikiUrl = $"https://en.wikipedia.org/wiki/{breed.Trim()}";
        DogPage.BackgroundColor = Color.FromArgb(await ImageHandler.GetImageColor(message));
        DogImage.Source = message;
        BreedLabel.Text = breed;
    }


    private async void CreateBreedPicker()
    {
        var breedList = new List<string> { "All" };
        breedList.AddRange(await Api.GetBreedList());
        
        var picker = new Picker { Title = "Breed" };
        picker.SelectedIndexChanged += (sender, e) => CurrentBreed = picker.SelectedItem.ToString().Replace("-", "/").Replace(" ", "");
        picker.ItemsSource = breedList;
        picker.TextColor = Color.FromArgb("#ffffff");
        picker.HorizontalOptions = LayoutOptions.Center;
        picker.SelectedIndex = 0;
        
        PickerHolder.Add(picker);

    }
    
    private void OnGetDogClickClicked(object sender, EventArgs e) => SetRandomDog(RandomUrl);
    private void Wiki_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new WikiPage(wikiUrl.Replace(" ", "_")));

}

