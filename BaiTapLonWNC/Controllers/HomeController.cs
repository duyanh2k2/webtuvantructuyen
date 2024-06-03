using BaiTapLonWNC.Models;
using BaiTapLonWNC.Models.Authen;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaiTapLonWNC.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaiTapLonWebContext _context;
        public HomeController(ILogger<HomeController> logger, BaiTapLonWebContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<TblBaiViet> baiViets = _context.TblBaiViets.OrderByDescending(t => t.MaBv).ToList();
            List<TblTrangCn> trangCns = _context.TblTrangCns.ToList();
            List<TblChuDe> chuDes = _context.TblChuDes.ToList();
            var home = new Home
            {
                BaiViets = baiViets,
                trangCns = trangCns,
                chuDes = chuDes

            };
            return View(home);
        }
        [Authen("False")]
        [HttpPost]
        public IActionResult Index(IFormFile file, TblBaiViet baiViet)
        {
            if (baiViet.TieuDe!=null && baiViet.NoiDung != null && file != null && file.Length > 0 && baiViet.VerifyKey.Length==10) {
                var user = HttpContext.Session.GetString("UserName");
                if (user != null)
                {
                    var idUS = _context.TblUsers.Where(u => u.UserName.Equals(user)).FirstOrDefault();
                    if (idUS != null)
                    {
                        var idTCN = _context.TblTrangCns.Where(t => t.MaUs == idUS.MaUs).FirstOrDefault();
                        if (idTCN != null)
                        {
                            baiViet.MaTcn = idTCN.MaTcn;
                            var finame = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", finame);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                 file.CopyTo(stream);
                                 baiViet.LinkAnh = finame;
                            }
                            _context.TblBaiViets.Add(baiViet);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            List<TblBaiViet> baiViets = _context.TblBaiViets.ToList();
            List<TblTrangCn> trangCns = _context.TblTrangCns.ToList();
            List<TblChuDe> chuDes = _context.TblChuDes.ToList();
            var home = new Home
            {
                BaiViets = baiViets,
                trangCns = trangCns,
                chuDes = chuDes

            };
            return View(home);

        }
        [Authen("False")]
        public IActionResult BaiVietTheoCD(int id)
        {
            List<TblBaiViet> baiViets = _context.TblBaiViets.Where(x=>x.MaChuDe==id).OrderByDescending(t=>t.MaBv).ToList();
            List<TblTrangCn> trangCns = _context.TblTrangCns.ToList();
            List<TblChuDe> chuDes = _context.TblChuDes.ToList();
            var home = new Home
            {
                BaiViets = baiViets,
                trangCns = trangCns,
                chuDes = chuDes

            };
            return View(home);
        }
        [Authen("False")]
        public IActionResult BaiVieSearch(string tieude)
        {
            if (tieude != null)
            {
                List<TblBaiViet> baiViets = _context.TblBaiViets.Where(x => x.TieuDe.Contains(tieude)).OrderByDescending(t => t.MaBv).ToList();
                List<TblTrangCn> trangCns = _context.TblTrangCns.ToList();
                List<TblChuDe> chuDes = _context.TblChuDes.ToList();
                var home = new Home
                {
                    BaiViets = baiViets,
                    trangCns = trangCns,
                    chuDes = chuDes

                };
                return View(home);
            }
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}