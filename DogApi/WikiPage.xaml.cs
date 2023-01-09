namespace DogApi;

public partial class WikiPage : ContentPage
{
	public WikiPage(string url)
	{
		InitializeComponent();
        View.Source = url;
    }
}