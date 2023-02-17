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
            var result = await Database.CreateTableAsync<FlashcardDbContext>();
            result = await Database.CreateTableAsync<GroupDbContext>();
            result = await Database.CreateTableAsync<CategoryDbContext>();
            return instance;
        });

        public FlashcardsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        //Database Operations
        public Task<int> SaveNoteCard(FlashcardDbContext noteCard)
        {
            if (noteCard.ID == 0)
            {
                noteCard.ID++;
                return Database.InsertAsync(noteCard);
            }
            else
            {
                return Database.UpdateAsync(noteCard);
            }
        }

        public Task<int> DeleteNoteCard(FlashcardDbContext noteCard)
        {
            return Database.DeleteAsync(noteCard);
        }

        public Task<List<FlashcardDbContext>> GetAllNoteCardsAsync()
        {
            return Database.Table<FlashcardDbContext>()
                           .ToListAsync();
        }

        public Task<FlashcardDbContext> GetNoteCard(FlashcardDbContext noteCard)
        {
            return Database.Table<FlashcardDbContext>()
                           .Where(card => card.ID == noteCard.ID)
                           .FirstAsync();
        }

        public Task<List<FlashcardDbContext>> GetNoteCardsInGroup(GroupDbContext group)
        {
            return Database.Table<FlashcardDbContext>()
                           .Where(card => card.GroupID == group.ID)
                           .ToListAsync();
        }

        public Task<int> CreateGroup(GroupDbContext group)
        {
            return Database.InsertAsync(group);
        }

        public Task<List<GroupDbContext>> GetGroupsInCategoryAsync(CategoryDbContext category)
        {
            return Database.Table<GroupDbContext>()
                           .Where(group => group.CategoryID == category.ID)
                           .ToListAsync();
        }

        public Task<int> CreateCategoryAsync(CategoryDbContext category)
        {
            return Database.InsertAsync(category);
        }

        public Task<List<CategoryDbContext>> GetAllCategories()
        {
            return Database.Table<CategoryDbContext>()
                           .ToListAsync();
        }

        public Task<CategoryDbContext> GetCategoryByID(int id)
        {
            return Database.Table<CategoryDbContext>()
                           .Where(c => c.ID == id)
                           .FirstAsync();
        }

    }
}

