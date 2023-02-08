
using System.Collections.Generic;

namespace LanguageNotes.Models
{
    public class NoteCard
    {
        public int ID { get; set; }

        public string FrontText { get; set; }

        public string Translation { get; set; }

        public string[] AltTranslations { get; set; }

        public int GroupID { get; set; }

        public int CategoryID { get; set; }
    }

    public class Group
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}

