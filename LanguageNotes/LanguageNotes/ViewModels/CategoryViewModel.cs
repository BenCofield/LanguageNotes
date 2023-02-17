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
		private readonly GroupRepo groupRepo;

        public Category Category { get; set; }

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
			groupRepo = new GroupRepo();
			Category = category;
        }

		internal async override void OnAppearing()
		{
			GroupsList = await groupRepo.LoadGroupsInCategory(Category);
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
			await groupRepo.CreateNewGroup(newGroup);
			GroupsList = await groupRepo.LoadGroupsInCategory(Category);
        }
	}
}

