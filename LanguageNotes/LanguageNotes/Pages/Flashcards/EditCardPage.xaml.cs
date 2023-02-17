using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.ViewModels;

namespace LanguageNotes.Pages
{	
	public partial class EditCardPage : ContentPage
	{
		EditCardViewModel viewModel;

		public EditCardPage(Flashcard flashcard)
		{
			InitializeComponent();
			viewModel = new EditCardViewModel(flashcard);
            BindingContext = viewModel;
		}
    }
}

