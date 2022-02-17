using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.ApiBook;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static ListBook instance;
        public static ListBook Instance { get => instance; }

        public ICommand ItemSelectedCommand { get; set; }

        public ICommand GoBack { get; set; }
        public ICommand GoTo { get; set; }

        static ListBook()
        {
            instance = new ListBook();
        }

        public ICommand DetailsBook { get; init; } = new RelayCommand(id => {
            foreach (BookPublic book in Ioc.Default.GetService<LibraryService>().Books)
            {
                if (book.Id == (int) id)
                {
                    Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
                }
            }
        });


        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<BookPublic> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ListBook()
        {
            Ioc.Default.GetService<LibraryService>().getBooks();
            ItemSelectedCommand = new RelayCommand(book => { Ioc.Default.GetService<LibraryService>().getBooks(); });
        }
    }   
   
}
