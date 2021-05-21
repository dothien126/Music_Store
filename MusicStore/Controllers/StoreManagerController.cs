using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using MusicStore.EntityContext;
using MusicStore.Services;
using MusicStore.Locators;

namespace MusicStore.Controllers
{

    [Authorize]
    public class StoreManagerController : Controller
    {
        private readonly IAlbumService albumService = ServiceLocator.AlbumService;
        private readonly IGenreService genreService = ServiceLocator.GenreService;

        // GET: /StoreManager/
        public ActionResult Index()
        {
            var albums = albumService.FindAlbums();
            return View(albums.ToList());
        }

        // GET: /StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.FindAlbumById(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: /StoreManager/Create
        public ActionResult Create()
        {

            ViewBag.ArtistId = new SelectList(albumService.FindAlbums(), "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genreService.FindGenres(), "GenreId", "Name");
            return View();
        }

        // POST: /StoreManager/Create
       
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
         
            if (ModelState.IsValid)
            {
                albumService.CreateAlbum(album);
                return RedirectToAction("Index");
            }
           
            ViewBag.ArtistId = new SelectList(albumService.FindAlbums(), "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genreService.FindGenres(), "GenreId", "Name");
            return View(album);
        }

        // GET: /StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.FindAlbumById(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(albumService.FindAlbums(), "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genreService.FindGenres(), "GenreId", "Name");
            return View(album);
        }

      
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                albumService.EditAlbum(album);
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(albumService.FindAlbums(), "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genreService.FindGenres(), "GenreId", "Name");
            return View(album);
        }

        // GET: /StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.FindAlbumById(id.Value);
            if (album == null)
            {
       
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            albumService.DeleteAlbum(id);
 
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
