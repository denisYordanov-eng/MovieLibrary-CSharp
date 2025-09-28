using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using IMDB.Models;

namespace IMDB.Controllers
{
    [ValidateInput(false)]
    public class FilmController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new IMDBDbContext())
            {
                var films = db.Films.ToList();
                return View(films);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                using (var db = new IMDBDbContext())
                {
                    db.Films.Add(film);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var dataBase = new IMDBDbContext())
            {
                var film = dataBase.Films.Find(id);
                if (film == null)
                {
                    return RedirectToAction("Index");
                }
                return View(film);
            }

        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                using (var database = new IMDBDbContext())
                {
                    Film filmFrimDb = database.Films.Find(id);

                    if (filmFrimDb == null)
                    {
                        return RedirectToAction("Index");
                    }

                    filmFrimDb.Name = filmModel.Name;
                    filmFrimDb.Genre = filmModel.Genre;
                    filmFrimDb.Director = filmModel.Director;
                    filmFrimDb.Year = filmModel.Year;

                    database.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var database = new IMDBDbContext())
            {
                Film film = database.Films.Find(id);

                if (film == null)
                {
                    return RedirectToAction("Index");
                }

                return View(film);
            }

        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Film filmModel)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var database = new IMDBDbContext())
            {
                Film film = database.Films.Find(id);

                if (film == null)
                {
                    return RedirectToAction("Index");
                }
                database.Films.Remove(film);
                database.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}