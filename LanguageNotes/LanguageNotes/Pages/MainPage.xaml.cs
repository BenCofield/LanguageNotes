using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LanguageNotes.Pages;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace LanguageNotes
{
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var noteCardsPage =  new FlashcardsHomePage();
            noteCardsPage.Title = "Flashcards";
            noteCardsPage.IconImageSource = "flashcard_tab_icon.png";

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            Children.Add(noteCardsPage);
            Children.Add( new NotebookPage() );
            Children.Add( new SettingsPage() );
        }
    }
}

