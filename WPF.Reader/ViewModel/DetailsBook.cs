using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.ApiBook;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookPublic CurrentBook { get; init; }

        public DetailsBook(BookPublic book)
        {
            CurrentBook = book;
            Ioc.Default.GetService<LibraryService>().getBookById(book.Id);
            //ItemSelectedCommand = new RelayCommand(book => { Ioc.Default.GetService<LibraryService>().getBookById(book.Id) });
        }

      //  public IObservable<BookPublic> Book => Ioc.Default.GetRequiredService<LibraryService>().Book;

    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookPublic() /*{ Title = "Test Book" }*/) { }

    }


}
