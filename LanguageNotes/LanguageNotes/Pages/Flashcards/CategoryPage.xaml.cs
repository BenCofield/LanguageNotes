using Xamarin.Forms;
using LanguageNotes.Models;
using LanguageNotes.db;

namespace LanguageNotes.Pages
{	
	public partial class CategoryPage : ContentPage
	{
		public Category Category { get; set; }

		public CategoryPage(Category category)
		{
			InitializeComponent();

			Category = category;

			Title = Category.Name;
		}
	}
}

