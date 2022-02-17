using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }




    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // A vous de faire comme BookController.List mais pour les genres !


        public ActionResult<IEnumerable<Genre>> List()
        {

            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché

            List<Genre> ListGenre = libraryDbContext.Genre.ToList();

            return View(ListGenre);
        }



        public ActionResult<Boolean> RedirectUpdateGenre(int genreId)
        {
            return RedirectToAction("Update", new { genreId = genreId });
        }


        // methode permet de mettre a jour un genre
           public ActionResult<Boolean> Update(int genreId, CreateGenreModel genre)
            {


                // fetch book with id
                var g = libraryDbContext.Genre.Single(g => g.Id == genreId);
               // get new data
               if (ModelState.IsValid)
               {

                g.Name = genre.Name;
                g.Description = genre.Description;


                  libraryDbContext.Genre.Update(g);
                  libraryDbContext.SaveChanges();

                 return RedirectToAction("List");


                }
                return View(new CreateGenreModel() { Id = genreId });

         }


        // methode permet de supprimer un genre

        public ActionResult<Boolean> RemoveGenre(int genreId)
        {

            var genre = libraryDbContext.Genre.Single(g => g.Id == genreId);
            Console.WriteLine("genre name " + genre.Name);
            libraryDbContext.Genre.Remove(genre);
            libraryDbContext.SaveChanges();

            return RedirectToAction("List");
        }


        // create Genre

        public ActionResult<CreateGenreModel> Create(CreateGenreModel genre)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {

            

                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                Genre g = new Genre()
                {
                    Name = genre.Name,
                    Description = genre.Description,
             
                };

                try
                {
                    libraryDbContext.Genre.Add(g);
                    libraryDbContext.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + "error man");
                    throw;
                }
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateGenreModel()  );
        }






    }
}
