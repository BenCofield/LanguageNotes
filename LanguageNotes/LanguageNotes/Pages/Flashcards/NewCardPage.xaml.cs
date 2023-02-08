using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.db;

namespace LanguageNotes.Pages
{	
	public partial class NewCardPage : ContentPage
	{
		private readonly FlashcardsDatabase _db;

		public NoteCard NoteCard { get; private set; }

		public NewCardPage ()
		{
			InitializeComponent ();
			NoteCard = new NoteCard();
			_db = new FlashcardsDatabase();

			BindingContext = NoteCard;
		}

        void On_Save(object sender, EventArgs e)
		{

			_db.SaveNoteCard(NoteCard);
		}

		int? CheckFieldsComplete()
		{
			if (NoteCard.FrontText == null)
			{

			}
			return 0;
		}
    }
}

