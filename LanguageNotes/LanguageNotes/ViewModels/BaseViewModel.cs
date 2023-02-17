using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;


namespace LanguageNotes.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
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

