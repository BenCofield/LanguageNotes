using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.ViewModels;

namespace LanguageNotes.Pages
{
    public partial class GroupPage : ContentPage
    {
        GroupViewModel viewModel;
        public GroupPage(Group group)
        {
            InitializeComponent();
            viewModel = new GroupViewModel(group);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}

