using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.db;

namespace LanguageNotes.Pages
{	
	public partial class NewCardPage : ContentPage
	{
		public NewCardPage(Group group)
		{
			InitializeComponent();
			Group = group;
			BindingContext = this;
		}

		public Group Group { get; set; }

		public Category Category { get; set; }

        public NoteCard NoteCard { get; private set; }

		string frontText;
		public string FrontText
		{
			get => frontText;
			set
			{
				if (frontText == value)
					return;

				frontText = value;
				OnPropertyChanged(nameof(FrontText));
			}
		}

		string translation;
		public string Translation
		{
			get => translation;
			set
			{
				if (translation == value)
					return;

				translation = value;
				OnPropertyChanged(nameof(Translation));
			}
		}

        async protected override void OnAppearing()
        {
			FlashcardsDatabase db = await FlashcardsDatabase.Instance;
			Category = await db.GetCategoryByID(Group.CategoryID);
			NoteCard = new NoteCard
			{
				GroupID = Group.ID,
				CategoryID = Category.ID
			};
        }

        async void On_Save(object sender, EventArgs e)
		{
			CheckFieldsComplete();

			FlashcardsDatabase db = await FlashcardsDatabase.Instance;
			await db.SaveNoteCard(NoteCard);
		}

		int CheckFieldsComplete()
		{
			if (string.IsNullOrWhiteSpace(FrontText))
				return -1;

			if (string.IsNullOrWhiteSpace(Translation))
				return -1;

			return 0;
		}
    }
}

