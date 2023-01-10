using Newtonsoft.Json.Linq;
using DogApi.BL;
using Color = Microsoft.Maui.Graphics.Color;

namespace DogApi;

public partial class MainPage : ContentPage
{
    private const string RandomUrl = "https://dog.ceo/api/breeds/image/random";
    private static string _wikiUrl = "https://en.wikipedia.org/wiki/Dog";
    private string _currentBreed = "All";
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
        if (_currentBreed != "All")
        {
            json = await Api.CallApi($"https://dog.ceo/api/breed/{_currentBreed.ToLower()}/images/random");
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
        Color color = Color.Parse(await ImageHandler.GetAvarageRGB(message));
        float colorLuminosity = color.GetLuminosity();
        Color textColor = colorLuminosity > 0.5 ? Color.FromArgb("#000000") : Color.FromArgb("#ffffff");

        _wikiUrl = $"https://en.wikipedia.org/wiki/{breed}";
        DogPage.BackgroundColor = color;

        DogImage.Source = message;
        BreedLabel.Text = breed;
        PickerBorder.Stroke = color;
        DogButton.TextColor = textColor;
        BreedLabel.TextColor = textColor;
        DogButton.BackgroundColor = color;
        Wiki.Text = $"Visit the {breed} Wiki";
    }


    private async void CreateBreedPicker()
    {
        var breedList = new List<string> { "All" };
        breedList.AddRange(await Api.GetBreedList());
        
        var picker = BreedPicker;
        picker.SelectedIndexChanged += (sender, e) => _currentBreed = picker.SelectedItem.ToString().Replace("-", "/").Replace(" ", "");
        picker.ItemsSource = breedList;
        picker.ItemsSource = picker.GetItemsAsArray();
        picker.SelectedIndex = 0;
    }
    
    private void OnGetDogClicked(object sender, EventArgs e) => SetRandomDog(RandomUrl);
    private void Wiki_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new WikiPage(_wikiUrl.Replace(" ", "_")));

}

