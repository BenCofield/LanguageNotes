using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageNotes.Models;

namespace LanguageNotes.db
{
	public class FlashcardsRepo
    { 
        public async Task<IList<Flashcard>> LoadFlashcardsInGroup(Group group)
        {
            var groupContext = FlashcardsDbContextFactory.GetGroup(group);
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
            var newCardContext = FlashcardsDbContextFactory.GetFlashcard(flashcard);
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
            var cardContext = FlashcardsDbContextFactory.GetFlashcard(flashcard);
            var db = await FlashcardsDatabase.Instance;
            await db.DeleteNoteCard(cardContext);
        }
    }
}

