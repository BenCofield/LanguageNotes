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
            var result = await Database.CreateTableAsync<FlashcardDboObject>();
            result = await Database.CreateTableAsync<GroupDboObject>();
            result = await Database.CreateTableAsync<CategoryDboObject>();
            return instance;
        });

        public FlashcardsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        //Database Operations
        public Task<int> SaveNoteCard(FlashcardDboObject noteCard)
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

        public Task<int> DeleteNoteCard(FlashcardDboObject noteCard)
        {
            return Database.DeleteAsync(noteCard);
        }

        public Task<List<FlashcardDboObject>> GetAllNoteCardsAsync()
        {
            return Database.Table<FlashcardDboObject>()
                           .ToListAsync();
        }

        public Task<FlashcardDboObject> GetNoteCard(FlashcardDboObject noteCard)
        {
            return Database.Table<FlashcardDboObject>()
                           .Where(card => card.ID == noteCard.ID)
                           .FirstAsync();
        }

        public Task<List<FlashcardDboObject>> GetNoteCardsInGroup(GroupDboObject group)
        {
            return Database.Table<FlashcardDboObject>()
                           .Where(card => card.GroupID == group.ID)
                           .ToListAsync();
        }

        public Task<int> CreateGroup(GroupDboObject group)
        {
            return Database.InsertAsync(group);
        }

        public Task<List<GroupDboObject>> GetGroupsInCategoryAsync(CategoryDboObject category)
        {
            return Database.Table<GroupDboObject>()
                           .Where(group => group.CategoryID == category.ID)
                           .ToListAsync();
        }

        public Task UpdateGroup(GroupDboObject group)
        {
            return Database.UpdateAsync(group);
        }

        public Task DeleteGroup(GroupDboObject group)
        {
            return Database.DeleteAsync<GroupDboObject>(group);
        }

        public Task<int> CreateCategoryAsync(CategoryDboObject category)
        {
            return Database.InsertAsync(category);
        }

        public Task<List<CategoryDboObject>> GetAllCategories()
        {
            return Database.Table<CategoryDboObject>()
                           .ToListAsync();
        }

        public Task<CategoryDboObject> GetCategoryByID(int id)
        {
            return Database.Table<CategoryDboObject>()
                           .Where(c => c.ID == id)
                           .FirstAsync();
        }

        public Task UpdateCategory(CategoryDboObject category)
        {
            return Database.UpdateAsync(category);
        }

        public Task DeleteCategory(CategoryDboObject category)
        {
            return Database.DeleteAsync<CategoryDboObject>(category);
        }
    }
}

