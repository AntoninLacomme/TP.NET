using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.ApiBook;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListGenre : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static ListGenre instance;
        public static ListGenre Instance { get => instance; }

        public ICommand ItemSelectedCommand { get; set; }

        static ListGenre()
        {
            instance = new ListGenre();
        }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<Genre> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        public ListGenre()
        {

            Ioc.Default.GetService<LibraryService>().getGenres();
            ItemSelectedCommand = new RelayCommand(book => { Ioc.Default.GetService<LibraryService>().getGenres(); });

        }



    }

}
