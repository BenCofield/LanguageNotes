using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

using LanguageNotes.db;
using LanguageNotes.Models;
using LanguageNotes.Pages;
using System.Threading.Tasks;

namespace LanguageNotes.ViewModels
{
	public class FlashcardsHomeViewModel : BaseViewModel
	{
        private readonly CategoryRepo categoryRepo;

        IList<Category> categories;
        public IList<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        //Commands
        public ICommand OpenCategoryPageCommand
        {
            get
            {
                return new Command<Category>((category) => OpenCategoryPage(category));
            }
        }

        public ICommand OnAddNewCategoryCommand
        {
            get {
                return new Command(() => OnAddNewCategory());
            }
        }

        //ctor
		public FlashcardsHomeViewModel()
		{
            categoryRepo = new CategoryRepo();
        }

        internal async override void OnAppearing()
        {
            Categories = await categoryRepo.LoadAllCategories();
        }

		async void OpenCategoryPage(Category category)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new CategoryPage(category)));
		}

        async void OnAddNewCategory()
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync(
                title: "New Category",
                message: "Give this category a name",
                initialValue: "Greetings",
                maxLength: 20,
                cancel: "cancel");

            if (string.IsNullOrEmpty(result))
                return;

            var newCat = new Category(result);
            await categoryRepo.CreateNewCategory(newCat);
            Categories = await categoryRepo.LoadAllCategories();
        }
    }
}

