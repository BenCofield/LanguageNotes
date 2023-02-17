using Xamarin.Forms;
using LanguageNotes.ViewModels;

namespace LanguageNotes.Pages
{	
	public partial class FlashcardsHomePage : ContentPage
	{
        FlashcardsHomeViewModel viewModel;
        public FlashcardsHomePage()
        {
            InitializeComponent();
            viewModel = new FlashcardsHomeViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}

