using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageNotes.Models;

namespace LanguageNotes.db
{
	public class CategoryRepo
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
            var newCategory = FlashcardsDbContextFactory.NewCategory(category);
            await db.CreateCategoryAsync(newCategory);
        }

    }
}

