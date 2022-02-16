using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            string loremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla consectetur felis leo, vitae pellentesque lectus aliquam vel. Aenean ac mauris eu diam tincidunt aliquet.Duis tempus ullamcorper urna, id congue magna rhoncus quis. Fusce sit amet erat ante.Vivamus vulputate facilisis euismod. Vivamus ut dictum quam. Pellentesque vel elit in mauris porttitor tristique in a ex. Maecenas eget tortor quis metus finibus hendrerit.";

          bookDbContext.Genre.AddRange(
                new Genre() { Name = "Comedie", Description = "Oeuvre comique ou proposant des procédés comiques." },
                new Genre() { Name ="Documentaire", Description = "Oeuvre documentaire." }, 
                new Genre() { Name = "Action", Description = "Oeuvre d'action." },
                new Genre() { Name = "Drama", Description = "Oeuvre Dramatique." },
                new Genre() { Name ="Thriller", Description="Oeuvre Policière." },
                new Genre() { Name = "Adventure", Description = "Oeuvre d'aventure." },
                new Genre() { Name = "Crime", Description = "Oeuvre Criminelle." },
                new Genre() { Name = "Romance", Description = "Oeuvre Romantique." },
                new Genre() { Name = "SF", Description = "Oeuvre de Science Fiction." }
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book()
                {
                    Name = "kalila wa demna",
                    Author = "Antonin",
                    Price = "49",
                    Content = loremIpsum,
                    //Kinds = bookDbContext.Genre.Where(x => { return (x.Name == "SF" | x.Name == "Romance") }).ToList()
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "SF") }
                },
                new Book()
                {
                    Name = "Harry Potter",
                    Author = "Khadijat",
                    Price = "49",
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Comedie") }
                },
                new Book()
                {
                    Name = "Voyage au bout de la nuit",
                    Author = "Lotfi",
                    Price = "49",
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Romance") }
                },
                new Book()
                {
                    Name = "Le seigneur des anneaux",
                    Author = "Rachid",
                    Price = "49",
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Thriller") }
                }
            );

            var authors = new List<String>()
            {
                "Antonin", "Khadijat", "Lotfi", "Rachid"
            };

            var length = bookDbContext.Genre.Count()-1;

            for (var i = 0; i < 1000; i++) {
                var genres = new List<Genre>();
                for (var j=0; j<new Random().Next(3)+1; j++)
                {
                    var myid = new Random().Next(length) + 1;
                    genres.Add(bookDbContext.Genre.Single(x => x.Id == myid));
                }

                bookDbContext.Books.Add(new Book()
                {
                    Name = RandomString (10, 20),
                    Author = authors[new Random().Next(authors.Count)],
                    Price = ((Double)(new Random().Next(10000)) / 100).ToString(),
                    Content = loremIpsum,
                    Kinds = genres
                });
            }
            
            bookDbContext.SaveChanges();
        }
        public static string RandomString(int min, int max)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, new Random().Next(max-min)+min)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}