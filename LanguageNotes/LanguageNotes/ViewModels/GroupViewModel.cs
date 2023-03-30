using System;
using System.Collections.Generic;
using Xamarin.Forms;

using LanguageNotes.Models;
using LanguageNotes.db;
using LanguageNotes.Pages;
using System.Windows.Input;
using System.Diagnostics;

namespace LanguageNotes.ViewModels
{
	public class GroupViewModel : BaseViewModel
	{
        Group group;
		public Group Group
        {
            get => group;
            set
            {
                group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        IList<Flashcard> flashcardsList;
        public IList<Flashcard> FlashcardsList
        {
            get => flashcardsList;
            set
            {
                flashcardsList = value;
                OnPropertyChanged(nameof(FlashcardsList));
            }
        }

        public ICommand OpenOptionsCommand
        {
            get => new Command(OpenOptions);
        }

        public ICommand EditFlashcardPageCommand
        {
            get => new Command<Flashcard>((flashcard) => OpenEditFlashcardPage(flashcard));
        }

        public ICommand NewFlashcardPageCommand
        {
            get => new Command(() => OpenNewFlashcardPage());
        }

        public ICommand DeleteFlashcardCommand
        {
            get => new Command<Flashcard>((flashcard) => DeleteFlashcard(flashcard));
        }

        public GroupViewModel(Group group)
        {
            Group = group;
        }

        internal async override void OnAppearing()
        {
            FlashcardsList = await repo.LoadFlashcardsInGroup(Group);
        }

        async void OpenOptions()
        {
            var action = await Application.Current.MainPage.DisplayActionSheet(
                "Options",
                "Cancel",
                null,
                "Rename Group",
                "Delete Group");

            switch(action)
            {
                case "Rename Group":
                    RenameGroup();
                    break;
                case "Delete Group":
                    DeleteGroup();
                    break;

                default:
                    break;
            }
            return;
        }

        async void RenameGroup()
        {
            var newName = await Application.Current.MainPage.DisplayPromptAsync(
                title: "Rename this group:",
                message: "",
                initialValue: "phrases",
                maxLength: 20,
                accept: "Submit",
                cancel: "Cancel");

            if (string.IsNullOrWhiteSpace(newName)) return;
            var temp = Group;
            temp.Name = newName;
            Group = temp;
            await repo.RenameGroup(Group);
            await repo.LoadFlashcardsInGroup(Group);
        }

        async void DeleteGroup()
        {
            var result = await Application.Current.MainPage.DisplayActionSheet(
                title: "Delete this group? This action cannot be undone",
                cancel: "Cancel",
                destruction: "Delete");
            if (result != "Delete") return;
            await repo.DeleteGroup(Group);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        async void OpenEditFlashcardPage(Flashcard flashcard)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditCardPage(flashcard));
        }

        async void OpenNewFlashcardPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditCardPage(new Flashcard(Group)));
        }

        async void DeleteFlashcard(Flashcard flashcard)
        {
            var result = await Application.Current.MainPage.DisplayActionSheet(
                title: "Delete this card?",
                cancel: "Cancel",
                destruction: "Delete",
                flashcard.FrontText);
            Debug.WriteLine("Action: " + result);
            if (result != "Delete")
                return;

            await repo.DeleteFlashcard(flashcard);
            FlashcardsList = await repo.LoadFlashcardsInGroup(Group);
        }
    }
}

