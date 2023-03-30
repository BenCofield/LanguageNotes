using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageNotes.Models;

namespace LanguageNotes.db
{
	public class FlashcardsRepo : IFlashcardsRepo
    {

        public async Task<IList<Category>> LoadAllCategories()
        {
            var db = await FlashcardsDatabase.Instance;
            var categoriesListContext = await db.GetAllCategories();

            var result = new List<Category>();
            foreach (var categoryContext in categoriesListContext)
            {
                result.Add(new Category(categoryContext));
            }

            return result;
        }

        public async Task CreateNewCategory(Category category)
        {
            var db = await FlashcardsDatabase.Instance;
            var newCategory = FlashcardsDboObjectCreator.NewCategory(category);
            await db.CreateCategoryAsync(newCategory);
        }

        public async Task<IList<Group>> LoadGroupsInCategory(Category category)
        {
            var categoryContext = FlashcardsDboObjectCreator.GetCategory(category);
            var db = await FlashcardsDatabase.Instance;
            var groupListContext = await db.GetGroupsInCategoryAsync(categoryContext);

            var result = new List<Group>();
            foreach (var groupContext in groupListContext)
            {
                result.Add(new Group(groupContext));
            }
            return result;
        }

        public async Task RenameCategory(Category category)
        {
            var db = await FlashcardsDatabase.Instance;
            var dboObject = FlashcardsDboObjectCreator.GetCategory(category);
            await db.UpdateCategory(dboObject);
        }

        public async Task DeleteCategory(Category category)
        {
            var db = await FlashcardsDatabase.Instance;
            var dboObject = FlashcardsDboObjectCreator.GetCategory(category);
            await db.DeleteCategory(dboObject);
        }

        public async Task CreateNewGroup(Group group)
        {
            var db = await FlashcardsDatabase.Instance;
            var newGroupContext = FlashcardsDboObjectCreator.NewGroup(group);
            await db.CreateGroup(newGroupContext);
        }

        public async Task RenameGroup(Group group)
        {
            var dboObject = FlashcardsDboObjectCreator.GetGroup(group);
            var db = await FlashcardsDatabase.Instance;
            await db.UpdateGroup(dboObject);
        }

        public async Task DeleteGroup(Group group)
        {
            var dboObject = FlashcardsDboObjectCreator.GetGroup(group);
            var db = await FlashcardsDatabase.Instance;
            await db.DeleteGroup(dboObject);
        }

        public async Task<IList<Flashcard>> LoadFlashcardsInGroup(Group group)
        {
            var groupContext = FlashcardsDboObjectCreator.GetGroup(group);
            var db = await FlashcardsDatabase.Instance;
            var listContext = await db.GetNoteCardsInGroup(groupContext);

            var resultList = new List<Flashcard>();
            foreach(var item in listContext)
            {
                resultList.Add(new Flashcard(item));
            }
            return resultList;
        }

        public async Task SaveFlashcard(Flashcard flashcard)
        {
            var newCardContext = FlashcardsDboObjectCreator.GetFlashcard(flashcard);
            var db = await FlashcardsDatabase.Instance;
            await db.SaveNoteCard(newCardContext);
        }

        public async Task<Category> GetCategory(int categoryID)
        {
            var db = await FlashcardsDatabase.Instance;
            var categoryContext = await db.GetCategoryByID(categoryID);
            return new Category(categoryContext);
        }

        public async Task DeleteFlashcard(Flashcard flashcard)
        {
            var cardContext = FlashcardsDboObjectCreator.GetFlashcard(flashcard);
            var db = await FlashcardsDatabase.Instance;
            await db.DeleteNoteCard(cardContext);
        }
    }
}

