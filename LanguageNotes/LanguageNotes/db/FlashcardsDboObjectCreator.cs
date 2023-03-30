using System;
using LanguageNotes.Models;
using Newtonsoft.Json;

namespace LanguageNotes.db
{
	public class FlashcardsDboObjectCreator
	{
		public static CategoryDboObject NewCategory(Category category)
		{
			return new CategoryDboObject
			{
				Name = category.Name
			};
		}

		public static CategoryDboObject GetCategory(Category category)
		{
			return new CategoryDboObject
			{
				ID = category.ID,
				Name = category.Name
			};
		}

		public static GroupDboObject NewGroup(Group group)
		{
			return new GroupDboObject
			{
				Name = group.Name,
				CategoryID = group.Category.ID
			};
        }

		public static GroupDboObject GetGroup(Group group)
		{
			return new GroupDboObject {
				ID = group.ID,
				Name = group.Name
			};
        }

		public static FlashcardDboObject GetFlashcard(Flashcard flashcard)
		{
			return new FlashcardDboObject
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

