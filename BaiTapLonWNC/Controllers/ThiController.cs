using BaiTapLonWNC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapLonWNC.Controllers
{
    public class ThiController : Controller
    {
        private readonly BaiTapLonWebContext _db;
        public ThiController(BaiTapLonWebContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var baiviet = _db.TblBaiViets.ToList(); 
            return View(baiviet);
        }
    }
}
