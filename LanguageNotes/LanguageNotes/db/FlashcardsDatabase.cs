using SQLite;
using LanguageNotes.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace LanguageNotes.db
{
    public class FlashcardsDatabase
    {
        static SQLiteConnection Database;

        private static readonly FlashcardsDatabase instance = new FlashcardsDatabase();

        public FlashcardsDatabase()
        {
            Database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            var result = Database.CreateTable<FlashcardDboObject>();
            result = Database.CreateTable<GroupDboObject>();
            result = Database.CreateTable<CategoryDboObject>();
        }

        //Database Operations
        public static int SaveNoteCard(Flashcard card)
        {
            var noteCard = new FlashcardDboObject(card);
            if (noteCard.ID == 0)
            {
                noteCard.ID++;
                return Database.Insert(noteCard);
            }
            else
            {
                return Database.Update(noteCard);
            }
        }

        public static int DeleteNoteCard(Flashcard noteCard)
        {
            var dbo = new FlashcardDboObject(noteCard);
            return Database.Delete(noteCard);
        }

        public static IList<Flashcard> GetAllNoteCards()
        {
            var cards = new List<Flashcard>();
            var dboList = Database.Table<FlashcardDboObject>()
                    .ToList();
            dboList.ForEach(obj => cards.Add(new Flashcard(obj)));
            return cards;
        }

        public static Flashcard GetNoteCard(Flashcard noteCard)
        {
            return new Flashcard(Database.Table<FlashcardDboObject>()
                                                .Where(card => card.ID == noteCard.ID)
                                                .First());
        }

        public static IList<Flashcard> GetNoteCardsInGroup(Group group)
        {
            var result = new List<Flashcard>();
            var dboList = Database.Table<FlashcardDboObject>()
                                        .Where(card => card.GroupID == group.ID)
                                        .ToList();
            dboList.ForEach(obj => result.Add(new Flashcard(obj)));
            return result;
        }

        public static int CreateGroup(Group group)
        {
            return Database.Insert(new GroupDboObject(group));
        }

        public static IList<Group> GetGroupsInCategory(Category category)
        {
            var result = new List<Group>();
            var dboList = Database.Table<GroupDboObject>()
                                        .Where(group => group.CategoryID == category.ID)
                                        .ToList();
            dboList.ForEach(obj => result.Add(new Group(obj)));
            return result;
        }

        public static int UpdateGroup(Group group)
        {
            return Database.Update(new GroupDboObject(group));
        }

        public static int DeleteGroup(Group group)
        {
            return Database.Delete(new GroupDboObject(group));
        }

        public static int CreateCategory(Category category)
        {
            return Database.Insert(new CategoryDboObject(category));
        }

        public static IList<Category> GetAllCategories()
        {
            var result = new List<Category>();
            var dboList = Database.Table<CategoryDboObject>()
                                        .ToList();
            dboList.ForEach(obj => result.Add(new Category(obj)));
            return result;
        }

        public static Category GetCategoryByID(int id)
        {
            return new Category(Database.Table<CategoryDboObject>()
                                 .Where(c => c.ID == id)
                                 .First());
        }

        public static int UpdateCategory(Category category)
        {
            return Database.Update(new CategoryDboObject(category));
        }

        public static int DeleteCategory(Category category)
        {
            return Database.Delete(new CategoryDboObject(category));
        }

        private object ToConcrete<T>(ref T dbo)
        {
            if (dbo is CategoryDboObject)
            {
                return new Category(dbo as CategoryDboObject);
            }
            if (dbo is GroupDboObject)
            {
                return new Group(dbo as GroupDboObject);
            }
            if (dbo is FlashcardDboObject)
            {
                return new Flashcard(dbo as FlashcardDboObject);
            }
            return null;
        }
    }
}

