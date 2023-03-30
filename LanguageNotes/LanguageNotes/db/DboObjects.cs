using SQLite;

namespace LanguageNotes.db
{
    [Table("flashcards")]
    public class FlashcardDboObject
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string FrontText { get; set; }

        public string Translation { get; set; }

        public string AltTranslationsJson { get; set; }

        public int GroupID { get; set; }

        public int CategoryID { get; set; }
    }

    [Table("groups")]
    public class GroupDboObject
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }
    }

    [Table("categories")]
    public class CategoryDboObject
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}

