
using System.Collections.Generic;
using SQLite;

namespace LanguageNotes.Models
{
    [Table("flashcards")]
    public class NoteCard
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string FrontText { get; set; }

        public string Translation { get; set; }

        public string AltTranslations { get; set; }

        public int GroupID { get; set; }

        public int CategoryID { get; set; }
    }

    [Table("groups")]
    public class Group
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }
    }

    [Table("categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}

