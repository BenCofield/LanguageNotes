using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.db;

namespace LanguageNotes.Pages
{
    public partial class GroupPage : ContentPage
    {
        public GroupPage(Group group)
        {
            InitializeComponent();
            Group = group;
            Title = group.Name;
            BindingContext = this;
        }

        public Group Group { get; set; }

        IList<NoteCard> noteCardsList;
        public IList<NoteCard> NoteCardsList
        {
            get => noteCardsList;
            set
            {
                if (noteCardsList == value)
                    return;

                noteCardsList = value;
                OnPropertyChanged(nameof(NoteCardsList));
            }
        }

        protected override async void OnAppearing()
        {
            FlashcardsDatabase db = await FlashcardsDatabase.Instance;
            NoteCardsList = await db.GetNoteCardsInGroup(Group);
        }

        async void Open_NewNotePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCardPage(Group));
        }

    }
}

