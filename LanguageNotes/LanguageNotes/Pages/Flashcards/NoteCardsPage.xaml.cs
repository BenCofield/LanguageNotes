using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.Pages;
using LanguageNotes.db;

namespace LanguageNotes
{	
	public partial class NoteCardsPage : ContentPage
	{
        public NoteCardsPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        IList<Category> categories;

        public IList<Category> Categories
        {
            get => categories;
            set
            {
                if (value == categories)
                    return;

                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        async protected override void OnAppearing()
        {
            FlashcardsDatabase db = await FlashcardsDatabase.Instance;
            Categories = await db.GetAllCategories();
        }

        async void Open_Category(object sender, EventArgs e)
        {
            var frame = (Frame)sender;

            if (frame.BindingContext is Category)
            {
                var category = frame.BindingContext as Category;
                await Navigation.PushAsync(new CategoryPage(category));
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        async void On_AddNewCategory(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Category", "Give this category a name", initialValue: "Greetings", maxLength: 20, cancel: "cancel");

            if (string.IsNullOrEmpty(result))
                return;

            var newCat = new Category()
            {
                Name = result
            };

            FlashcardsDatabase db = await FlashcardsDatabase.Instance;
            await db.CreateCategoryAsync(newCat);
            Categories = await db.GetAllCategories();
        }
    }
}

