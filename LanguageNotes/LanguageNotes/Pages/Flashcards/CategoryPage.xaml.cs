using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.db;
using System.Collections.Generic;
using System;

namespace LanguageNotes.Pages
{	
	public partial class CategoryPage : ContentPage
	{
		public CategoryPage(Category category)
		{
			InitializeComponent();

			Category = category;
			Title = Category.Name;
			BindingContext = this;
		}

        public Category Category { get; set; }

		IList<Group> groups;
		public IList<Group> Groups
		{
			get => groups;
			set
			{
				if (groups == value)
					return;

				groups = value;
				OnPropertyChanged(nameof(Groups));
			}
		}

        async protected override void OnAppearing()
		{
            FlashcardsDatabase db = await FlashcardsDatabase.Instance;
			Groups = await db.GetGroupsInCategoryAsync(Category);
        }

        async void Open_GroupPage(object sender, EventArgs e)
        {
			var frame = (Frame)sender;
			if (frame.BindingContext is Group)
			{
				var group = frame.BindingContext as Group;
                await Navigation.PushAsync(new GroupPage(group));
            }
			else
			{
				throw new InvalidCastException();
			}
        }

		async void On_AddNewGroup(object sender, EventArgs e)
		{
			var name = await DisplayPromptAsync("New Group", "Give this group a name", initialValue: "Phrases", maxLength: 20, cancel: "Cancel"); ;

			if (string.IsNullOrEmpty(name))
				return;

			var newGroup = new Group()
			{
				CategoryID = Category.ID,
				Name = name
			};
			FlashcardsDatabase db = await FlashcardsDatabase.Instance;
			await db.CreateGroup(newGroup);
			Groups = await db.GetGroupsInCategoryAsync(Category);
        }
    }
}

