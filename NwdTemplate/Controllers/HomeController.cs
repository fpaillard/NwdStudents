using Nwd.BackOffice.Impl;
using Nwd.BackOffice.Model;
using System.Web.Mvc;

namespace NwdTemplate.Controllers
{
    public class HomeController : Controller
    {
        private AlbumRepository repoAlbums = new AlbumRepository();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public void AddTrack(Album album, Track track)
        {
            album.Tracks.Add(track);
            repoAlbums.EditAlbum(this.Server, album);
        }

        public ActionResult Album()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public void CreateAlbum(Album album)
        {
            if (album.Title != null && !repoAlbums.AlbumExists(album))
            {
                repoAlbums.CreateAlbum(album, this.Server);
                ViewBag.Message = "succeed";
            }
            else
            {
                ViewBag.Message = "failed";
            }
        }

        [HttpPost]
        public ActionResult EditAlbum(Album album)
        {
            if (album.Id == 0)
            {
                CreateAlbum(album);
                ViewData.Model = album;
            }
            else
            {
                var albumAModifier = repoAlbums.GetAlbumForEdit(album.Id);
                albumAModifier.Title = album.Title;
                albumAModifier.Tracks = album.Tracks;

                foreach (var track in albumAModifier.Tracks)
                {
                    track.AlbumId = album.Id;
                }

                repoAlbums.EditAlbum(this.Server, albumAModifier);

                ViewData.Model = albumAModifier;
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditAlbum(int id)
        {
            if (id == 0)
            {
                ViewData.Model = new Album();
                return View();
            }
            var albumAModifier = repoAlbums.GetAlbumForEdit(id);
            ViewData.Model = albumAModifier;

            return View();
        }

        public ActionResult Index()
        {
            var list = repoAlbums.GetAllAlbums();
            ViewData.Model = list;
            return View();
        }
    }
}