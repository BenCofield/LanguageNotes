using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using LanguageNotes.db;
using LanguageNotes.Models;
using LanguageNotes.Pages;
using static SQLite.SQLite3;
using System.Threading.Tasks;

namespace LanguageNotes.ViewModels
{
	public class CategoryViewModel : BaseViewModel
	{
        Category category;
        public Category Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

		IList<Group> groupsList;
		public IList<Group> GroupsList
		{
			get => groupsList;
			set
			{
				groupsList = value;
				OnPropertyChanged(nameof(GroupsList));
			}
		}

        public ICommand OpenOptionsCommand
        {
            get => new Command(OpenOptions);
        }

        public ICommand OpenGroupPageCommand
		{
			get {
				return new Command<Group>((group) => OpenGroupPage(group));
            }
		}

		public ICommand OnAddNewGroupCommand
		{
			get
			{
				return new Command(OnAddNewGroup);
            }
		}
        
        public CategoryViewModel(Category category)
		{
			Category = category;
        }

		internal override void OnAppearing()
		{
            GroupsList = FlashcardsDatabase.GetGroupsInCategory(Category);
		}

        async void OpenOptions()
        {
            var action = await Application.Current.MainPage.DisplayActionSheet(
                "Options",
                "Cancel",
                null,
                "Rename Category",
                "Delete Category");

            switch (action)
            {
                case "Rename Category":
                    RenameCategory();
                    break;
                case "Delete Category":
                    DeleteCategory();
                    break;

                default:
                    break;
            }
            return;
        }

        async void RenameCategory()
        {
            var newName = await Application.Current.MainPage.DisplayPromptAsync(
                title: "Rename This Category:",
                message: "",
                initialValue: "Greetings",
                maxLength: 20,
                accept: "Submit",
                cancel: "Cancel");

            if (string.IsNullOrWhiteSpace(newName)) return;
            if (newName == "Cancel") return;

            var temp = this.Category;
            temp.Name = newName;
            this.Category = temp;
            FlashcardsDatabase.UpdateCategory(Category);
        }

        async void DeleteCategory()
        {
            var result = await Application.Current.MainPage.DisplayActionSheet(
                title: "Delete this category? This action cannot be undone",
                cancel: "Cancel",
                destruction: "Delete");

            if (result != "Delete") return;
            FlashcardsDatabase.DeleteCategory(Category);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        async void OpenGroupPage(Group group)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new GroupPage(group));
		}

		async void OnAddNewGroup()
		{
			string result = await Application.Current.MainPage.DisplayPromptAsync(
                "New Group",
                "Give this group a name",
                initialValue: "Phrases",
                maxLength: 20,
				cancel: "cancel");

            if (string.IsNullOrEmpty(result))
                return;

			var newGroup = new Group(result, Category);
			FlashcardsDatabase.CreateGroup(newGroup);
			GroupsList = FlashcardsDatabase.GetGroupsInCategory(Category);
        }
	}
}

