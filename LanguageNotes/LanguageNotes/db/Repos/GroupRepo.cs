using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageNotes.Models;

namespace LanguageNotes.db
{
	public class GroupRepo
	{
        public async Task<IList<Group>> LoadGroupsInCategory(Category category)
        {
            var categoryContext = FlashcardsDbContextFactory.GetCategory(category);
            var db = await FlashcardsDatabase.Instance;
            var groupListContext = await db.GetGroupsInCategoryAsync(categoryContext);

            var result = new List<Group>();
            foreach (var groupContext in groupListContext)
            {
                result.Add(new Group(groupContext));
            }
            return result;
        }

        public async Task CreateNewGroup(Group group)
        {
            var db = await FlashcardsDatabase.Instance;
            var newGroupContext = FlashcardsDbContextFactory.NewGroup(group);
            await db.CreateGroup(newGroupContext);
        }
    }
}

