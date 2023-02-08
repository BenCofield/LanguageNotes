using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.Pages;

namespace LanguageNotes
{	
	public partial class NoteCardsPage : ContentPage
	{
		public IList<Category> Categories { get; private set; }

		public NoteCardsPage()
		{
			InitializeComponent();

            Categories = new List<Category>
            {
                new Category()
                {
                    Name = "Greetings"
                },

                new Category()
                {
                    Name = "Numbers"
                }
            };

            BindingContext = this;
        }

        async void Open_NewCardPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCardPage());
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
    }
}

