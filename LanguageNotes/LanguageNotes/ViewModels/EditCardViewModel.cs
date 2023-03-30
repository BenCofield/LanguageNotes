
using System.ComponentModel;
using System.Windows.Input;
using LanguageNotes.Models;
using Xamarin.Forms;
using LanguageNotes.db;

namespace LanguageNotes.ViewModels
{
    public class EditCardViewModel : BaseViewModel
    {
        private Flashcard NoteCard { get; set; }

        public string FrontText
        {
            get => NoteCard.FrontText;
            set
            {
                if (NoteCard.FrontText == value)
                    return;
                NoteCard.FrontText = value;
                OnPropertyChanged(nameof(FrontText));
            }
        }

        public string Translation
        {
            get => NoteCard.Translation;
            set
            {
                if (NoteCard.Translation == value)
                    return;

                NoteCard.Translation = value;
                OnPropertyChanged(nameof(Translation));
            }
        }

        public ICommand SaveCommand
        {
            get => new Command(() => Save());
        }
        public ICommand EraseCommand
        {
            get => new Command(() => Erase());
        }

        public EditCardViewModel(Flashcard note)
        {
            NoteCard = note;
        }

        async void Save()
        {
            await repo.SaveFlashcard(NoteCard);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public void Erase()
        {
            FrontText = string.Empty;
            Translation = string.Empty;
        }
    }
}

