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
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => 
        {
            Book myBook = Ioc.Default.GetService<LibraryService>().getBookById((int)x);

            Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(myBook); 
        });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookPublic CurrentBook { get; init; }

        public DetailsBook(BookPublic book)
        {
            CurrentBook = book;
        }

    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookPublic() /*{ Title = "Test Book" }*/) { }

    }


}
