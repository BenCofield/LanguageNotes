using SQLite;
using LanguageNotes.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LanguageNotes.db
{
    public class FlashcardsDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<FlashcardsDatabase> Instance = new AsyncLazy<FlashcardsDatabase>(async () =>
        {
            var instance = new FlashcardsDatabase();
            var result = await Database.CreateTableAsync<NoteCard>();
            result = await Database.CreateTableAsync<Group>();
            result = await Database.CreateTableAsync<Category>();
            return instance;
        });

        public FlashcardsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }



        //Database Operations
        public Task<int> SaveNoteCard(NoteCard noteCard)
        {
            return Database.InsertOrReplaceAsync(noteCard);
        }

        public Task<int> DeleteNoteCard(NoteCard noteCard)
        {
            return Database.DeleteAsync<NoteCard>(noteCard);
        }

        public Task<List<NoteCard>> GetAllNoteCardsAsync()
        {
            return Database.Table<NoteCard>()
                           .ToListAsync();
        }

        public Task<NoteCard> GetNoteCard(NoteCard noteCard)
        {
            return Database.Table<NoteCard>()
                           .Where(card => card.ID == noteCard.ID)
                           .FirstAsync();
        }

        public Task<List<NoteCard>> GetNoteCardsInGroup(Group group)
        {
            return Database.Table<NoteCard>()
                           .Where(card => card.GroupID == group.ID)
                           .ToListAsync();
        }

        public Task<int> CreateGroup(Group group)
        {
            return Database.InsertAsync(group);
        }

        public Task<List<Group>> GetGroupsInCategoryAsync(Category category)
        {
            return Database.Table<Group>()
                           .Where(group => group.CategoryID == category.ID)
                           .ToListAsync();
        }

        public Task<int> CreateCategoryAsync(Category category)
        {
            return Database.InsertAsync(category);
        }

        public Task<List<Category>> GetAllCategories()
        {
            return Database.Table<Category>()
                           .ToListAsync();
        }

        public Task<Category> GetCategoryByID(int id)
        {
            return Database.Table<Category>()
                           .Where(c => c.ID == id)
                           .FirstAsync();
        }
    }
}

