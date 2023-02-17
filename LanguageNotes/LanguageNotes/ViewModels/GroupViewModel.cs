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
        private readonly FlashcardsRepo repo;

		public Group Group { get; set; }

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

        public ICommand EditFlashcardPageCommand
        {
            get {
                return new Command<Flashcard>((flashcard) => OpenEditFlashcardPage(flashcard));
            }
        }

        public ICommand NewFlashcardPageCommand
        {
            get
            {
                return new Command(() => OpenNewFlashcardPage());
            }
        }

        public ICommand DeleteFlashcardCommand
        {
            get
            {
                return new Command<Flashcard>((flashcard) => DeleteFlashcard(flashcard));
            }
        }

        public GroupViewModel(Group group)
        {
            Group = group;
            repo = new FlashcardsRepo();
        }

        internal async override void OnAppearing()
        {
            FlashcardsList = await repo.LoadFlashcardsInGroup(Group);
        }

        async void OpenEditFlashcardPage(Flashcard flashcard)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditCardPage(flashcard));
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

        async void OpenNewFlashcardPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditCardPage(new Flashcard(Group)));
        }
    }
}

