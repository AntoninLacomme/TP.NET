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

            string loremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla consectetur felis leo, vitae pellentesque lectus aliquam vel. Aenean ac mauris eu diam tincidunt aliquet.Duis tempus ullamcorper urna, id congue magna rhoncus quis. Fusce sit amet erat ante.Vivamus vulputate facilisis euismod. Vivamus ut dictum quam. Pellentesque vel elit in mauris porttitor tristique in a ex. Maecenas eget tortor quis metus finibus hendrerit.

Mauris porta feugiat nisi, sed mattis sapien. Ut tincidunt justo vitae erat ullamcorper, id efficitur urna pulvinar.Mauris vel imperdiet mauris. Vestibulum pellentesque arcu a nisl condimentum aliquet.Sed eget massa at enim ornare elementum quis sit amet tellus.Nulla non porta nibh, ut fermentum tellus. Nullam ut finibus purus. Etiam vitae orci tellus.

Donec laoreet sollicitudin enim sed ornare. Suspendisse convallis magna ante, vel ullamcorper ante facilisis eu. Phasellus mattis massa in bibendum scelerisque. Cras sodales fringilla nunc id volutpat. Ut id tempor nibh. Donec eu dui eu nisl suscipit rhoncus.Duis accumsan feugiat tellus, sed blandit libero convallis a. Morbi et tortor sit amet risus facilisis convallis. Vestibulum dui nisi, semper a faucibus non, ornare nec nulla. Suspendisse enim odio, ultricies vitae purus a, lobortis volutpat velit. Suspendisse aliquam leo lacus, placerat pharetra velit porttitor vitae.

Quisque lacinia, elit in consequat commodo, dolor lacus bibendum mi, sit amet venenatis tellus est vel augue.Integer lobortis mi ut dolor sollicitudin imperdiet.Donec ut efficitur magna, in hendrerit augue. Morbi sem lorem, finibus ac tellus non, efficitur pharetra odio. Quisque ut convallis justo. Morbi quis nunc semper, iaculis quam eget, dapibus lectus.Etiam cursus at eros ut ornare. In sit amet elit dapibus, tincidunt turpis a, rutrum massa. Aliquam malesuada quam eget metus tincidunt egestas.Morbi volutpat sagittis venenatis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Donec eu dui ac diam bibendum pretium.Nunc sed interdum dolor, et dapibus dolor. Phasellus ipsum neque, condimentum sit amet fringilla non, malesuada ut quam.Etiam eget porttitor lorem.

Donec turpis metus, aliquam sit amet dictum condimentum, feugiat vel orci.Donec volutpat tempus diam, vel mattis ex pharetra at. Vestibulum interdum bibendum risus. Mauris sollicitudin iaculis quam nec dictum. Phasellus in pharetra nulla, in venenatis ipsum. Suspendisse ligula nisi, venenatis id lacinia et, iaculis id augue. Phasellus a odio ut turpis lacinia molestie.Nulla scelerisque elit metus, eget gravida ex porta eget. Donec euismod, ipsum id hendrerit efficitur, quam metus volutpat nisl, ac efficitur est odio sed mauris.";

          bookDbContext.Genre.AddRange(
                new Genre()
                {
                    Name = "SF",
                    Description = "SF bla bla bla"
                },
                new Genre()
                {
                    Name="Classique",
                    Description = "Classique bla bla bla"
                },
                new Genre()
                {
                    Name = "Romance",
                    Description = "Romance bla bla bla"
                },
                new Genre()
                {
                    Name = "Thriller",
                    Description = " Thriller bla bla bla"
                }
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book()
                {
                    Name = "kalila wa demna",
                    Author = "Antonin",
                    Price = 49,
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "SF") }
                },
                new Book()
                {
                    Name = "Harry Potter",
                    Author = "Khadijat",
                    Price = 49,
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Classique") }
                },
                new Book()
                {
                    Name = "Voyage au bout de la nuit",
                    Author = "Lotfi",
                    Price = 49,
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Romance") }
                },
                new Book()
                {
                    Name = "Le seigneur des anneaux",
                    Author = "Rachid",
                    Price = 49,
                    Content = loremIpsum,
                    Kinds = new List<Genre> { bookDbContext.Genre.Single(genre => genre.Name == "Thriller") }
                }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}