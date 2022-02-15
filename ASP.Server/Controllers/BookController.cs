﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Price")]
        public string Price { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre

        // Liste des genres séléctionné par l'utilisateur
        public List<Genre> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;


        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {

            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché

            List<Book> ListBooks = libraryDbContext.Books.Include(g=>g.Kinds).ToList();
          
            return ListBooks;
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                //List<Genre> genres = book.Genres.ToList();
                Console.WriteLine("test Genres");
                //Console.WriteLine(book.Genres.ToList());

                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                Book b = new Book()
                {
                    Name = book.Name,
                    Author = book.Author,
                    Price = book.Price,
                    Content = book.Content,
                };

                try
                {
                   libraryDbContext.Add(b);
                   libraryDbContext.SaveChanges();
                   Console.WriteLine("ok man !");
                    Redirect("/");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + "error man");
                    throw;
                }
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = libraryDbContext.Genre.ToList() } );
        }
    }
}
