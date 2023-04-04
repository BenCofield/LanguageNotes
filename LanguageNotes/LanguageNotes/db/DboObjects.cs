using LanguageNotes.Models;
using SQLite;

namespace LanguageNotes.db
{
    [Table("flashcards")]
    public class FlashcardDboObject
    {
        [PrimaryKey, Column("_id")]
        public int ID { get; set; }

        public string FrontText { get; set; }

        public string Translation { get; set; }

        public string AltTranslationsJson { get; set; }

        public int GroupID { get; set; }

        public int CategoryID { get; set; }

        public FlashcardDboObject()
        {

        }

        public FlashcardDboObject(Flashcard card)
        {
            ID = card.ID;
            FrontText = card.FrontText;
            Translation = card.Translation;
            AltTranslationsJson = Newtonsoft.Json.JsonConvert.SerializeObject(card.AltTranslations);
            GroupID = card.Group.ID;
            CategoryID = card.Category.ID;
        }
    }

    [Table("groups")]
    public class GroupDboObject
    {
        [PrimaryKey, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public GroupDboObject()
        {

        }

        public GroupDboObject(Group group)
        {
            ID = group.ID;
            Name = group.Name;
            CategoryID = group.Category.ID;
        }
    }

    [Table("categories")]
    public class CategoryDboObject
    {
        [PrimaryKey, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }

        public CategoryDboObject()
        {

        }

        public CategoryDboObject(Category category)
        {
            ID = category.ID;
            Name = category.Name;
        }
    }
}

