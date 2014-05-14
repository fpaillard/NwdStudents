using Nwd.BackOffice.Impl;
using System.Web.Mvc;

namespace NwdTemplate.Controllers
{
    public class AlbumController : Controller
    {
        private AlbumRepository repoAlbums = new AlbumRepository();

        public ActionResult EditAlbum(int id)
        {
            var albumAModifier = repoAlbums.GetAlbumForEdit(id);
            ViewData.Model = albumAModifier;

            repoAlbums.EditAlbum(this.Server, albumAModifier);
            return View();
        }
    }
}