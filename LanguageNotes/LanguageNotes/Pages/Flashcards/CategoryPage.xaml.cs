﻿using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.ViewModels;

namespace LanguageNotes.Pages
{	
	public partial class CategoryPage : ContentPage
	{
		CategoryViewModel viewModel;
		public CategoryPage(Category category)
		{
			InitializeComponent();
            viewModel = new CategoryViewModel(category);
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}

