
using System.ComponentModel;
using System.Windows.Input;

namespace LanguageNotes.ViewModels
{
    public class FlashcardsViewModel : INotifyPropertyChanged
    {
        ICommand tapCommand;
        public FlashcardsViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

