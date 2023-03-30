using System;
using LanguageNotes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageNotes.db
{
	public interface IFlashcardsRepo
	{
        Task<Category> GetCategory(int categoryID);

        Task<IList<Category>> LoadAllCategories();

        Task CreateNewCategory(Category category);

        Task<IList<Group>> LoadGroupsInCategory(Category category);

        Task RenameCategory(Category category);

        Task DeleteCategory(Category category);

        Task CreateNewGroup(Group group);

        Task RenameGroup(Group group);

        Task DeleteGroup(Group group);

        Task<IList<Flashcard>> LoadFlashcardsInGroup(Group group);

        Task SaveFlashcard(Flashcard flashcard);

        Task DeleteFlashcard(Flashcard flashcard);
    }
}

