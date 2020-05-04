using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImagePost.Models;
using Microsoft.Extensions.Configuration;
using ImagePost.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ImagePost.Controllers
{
    public class HomeController : Controller
    {
        private string _conStr;

        public HomeController(IConfiguration configuration)
        {
            _conStr = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new ImageRepository(_conStr);
            return View(repo.GetImages());
        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(Image image)
        {
            var repo = new ImageRepository(_conStr);
            image.DatePosted = DateTime.Now;
            repo.AddImage(image);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult AddLike(int imgId)
        {
            var repo = new ImageRepository(_conStr);            
            repo.AddLike(imgId);

            List<int> likedIds = HttpContext.Session.Get<List<int>>("likedids") ?? new List<int>();
            likedIds.Add(imgId);
            HttpContext.Session.Set("likedids", likedIds);

            return Json("");
        }

        public IActionResult Enlarged(int imgId)
        {
            var repo = new ImageRepository(_conStr);
            var vm = new EnlargedViewModel();
            vm.Image = repo.GetById(imgId);

            if (HttpContext.Session.Get<List<int>>("likedids") != null)
            {
                var likedIds = HttpContext.Session.Get<List<int>>("likedids");
                vm.CanLike = !likedIds.Any(i => i == imgId);
            }
            else
            {
                vm.CanLike = true;
            }

            return View(vm);
        }

        public IActionResult GetLikes(int imgId)
        {
            var repo = new ImageRepository(_conStr);
            return Json(repo.GetLikes(imgId));
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
