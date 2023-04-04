using System;
using System.Collections.Generic;
using LanguageNotes.db;
using Newtonsoft.Json;

namespace LanguageNotes.Models
{
	public class Flashcard
	{
		public int ID { get; set; } = 0;
		public string FrontText { get; set; }
		public string Translation { get; set; }
		public List<string> AltTranslations { get; set; }
		public Category Category { get; set; }
		public Group Group { get; set; }

        public Flashcard()
		{

		}

		public Flashcard(Group group)
		{
			Group = group;
			Category = group.Category;
		}

		public Flashcard(FlashcardDboObject flashcardDbContext)
		{
			ID = flashcardDbContext.ID;
			FrontText = flashcardDbContext.FrontText;
			Translation = flashcardDbContext.Translation;
			AltTranslations = JsonConvert.DeserializeObject<List<string>>(flashcardDbContext.AltTranslationsJson);
			Category = new Category
			{
				ID = flashcardDbContext.CategoryID
			};

			Group = new Group()
			{
				ID = flashcardDbContext.GroupID
			};
		}
	}

	public class Group
	{
		public int ID { get; set; } = 0;	
		public string Name { get; set; }
		public Category Category { get; set; }
        public List<Flashcard> Flashcards { get; set; }

		public Group()
		{

		}

		public Group(string name, Category category)
		{
			Name = name;
			Category = category;
		}

		public Group(string name, int categoryID)
		{
			Name = name;
			Category = new Category
			{
				ID = categoryID
			};
		}

		public Group (GroupDboObject groupDbContext)
		{
			ID = groupDbContext.ID;
			Name = groupDbContext.Name;
			Category = new Category()
			{
				ID = groupDbContext.CategoryID
			};
		}
	}

	public class Category
	{
		public int ID { get; set; } = 0;
		public string Name { get; set; }
		public string ImagePath { get; set; }
        public List<Group> Groups { get; set; }

		public Category()
		{

		}

		public Category(string name)
		{
			Name = name;
		}

        public Category(CategoryDboObject categoryDbo)
		{
			ID = categoryDbo.ID;
			Name = categoryDbo.Name;
		}
	}
}

