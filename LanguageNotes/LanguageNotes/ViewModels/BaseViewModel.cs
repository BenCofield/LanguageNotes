using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LanguageNotes.db;
using Xamarin.Forms;


namespace LanguageNotes.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
        internal readonly IFlashcardsRepo repo = new FlashcardsRepo();

        public BaseViewModel()
		{
            
		}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal virtual void OnAppearing() { }
        internal virtual void OnDisappearing() { }
    }
}

