using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using WPF.Reader.ApiBook;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouer et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()

        static System.Net.Http.HttpClient getHttpClient ()
        {
            return new System.Net.Http.HttpClient() { BaseAddress = new System.Uri("https://localhost:5001") };
        }

        async public void getBooks()
        {
            var client = new ApiBook.Client(getHttpClient ());
            var result = await client.ApiBookGetBooksAsync(null, 10, null);

            Application.Current.Dispatcher.Invoke(() =>
            {
                Books.Clear();
                foreach (BookPublic book in result)
                {
                    Books.Add(book);
                }
            });
        }

        async public void getBookById (int id)
        {
            var client = new ApiBook.Client(getHttpClient());
            Book result = await client.ApiBookGetBookAsync(id);

            Application.Current.Dispatcher.Invoke(() =>
            {
                BookById = result;
            });
        }

        async public void getGenres()
        {
            var client = new ApiBook.Client(getHttpClient());
            var result = await client.ApiGenreGetGenresAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Genres.Clear();
                foreach (Genre genre in result)
                {
                    Genres.Add(genre);
                }
            });
        }

        public ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>() { };
        public ObservableCollection<BookPublic> Books { get; set; } = new ObservableCollection<BookPublic>() {

            new BookPublic() {Name="Hello ?"},
            new BookPublic(),
            new BookPublic()
        };

        public Book BookById { get; set; } = new Book();



        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
    }
}
