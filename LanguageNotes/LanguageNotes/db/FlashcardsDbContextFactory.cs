using System;

using LanguageNotes.Models;
using Newtonsoft.Json;

namespace LanguageNotes.db
{
	public class FlashcardsDbContextFactory
	{
		public static CategoryDbContext NewCategory(Category category)
		{
			return new CategoryDbContext
			{
				Name = category.Name
			};
		}

		public static CategoryDbContext GetCategory(Category category)
		{
			return new CategoryDbContext
			{
				ID = category.ID,
				Name = category.Name
			};
		}

		public static GroupDbContext NewGroup(Group group)
		{
			return new GroupDbContext
			{
				Name = group.Name,
				CategoryID = group.Category.ID
			};
        }

		public static GroupDbContext GetGroup(Group group)
		{
			return new GroupDbContext {
				ID = group.ID,
				Name = group.Name
			};
        }

		public static FlashcardDbContext GetFlashcard(Flashcard flashcard)
		{
			return new FlashcardDbContext
			{
				ID = flashcard.ID,
				FrontText = flashcard.FrontText,
				Translation = flashcard.Translation,
				AltTranslationsJson = JsonConvert.SerializeObject(flashcard.AltTranslations),
				CategoryID = flashcard.Category.ID,
				GroupID = flashcard.Group.ID
			};
		}
	}
}

