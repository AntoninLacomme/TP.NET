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


        async public void getBooks()
        {
            var client = new ApiBook.Client(new System.Net.Http.HttpClient() { BaseAddress = new System.Uri("https://localhost:5001") });
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
        public ObservableCollection<BookPublic> Books { get; set; } = new ObservableCollection<BookPublic>() {
            new BookPublic(),
            new BookPublic(),
            new BookPublic()
        };

        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
    }
}
