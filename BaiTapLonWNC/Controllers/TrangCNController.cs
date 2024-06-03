using BaiTapLonWNC.Models;
using BaiTapLonWNC.Models.Authen;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace BaiTapLonWNC.Controllers
{
    [Authen("False")]
    public class TrangCNController : Controller
    {
        private readonly BaiTapLonWebContext _context;
        public TrangCNController(BaiTapLonWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(int maTCN)
        {

            HttpContext.Session.SetInt32("maTCN", maTCN);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var trangcn = _context.TblTrangCns.Where(t => t.MaTcn == maTCN).FirstOrDefault();
                if (trangcn != null)
                {
                    var us = _context.TblUsers.Where(u => u.MaUs == trangcn.MaUs).FirstOrDefault();
                    if (us != null)
                    {
                        HttpContext.Session.SetString("UserName2", us.UserName.ToString());
                        var baiviet = _context.TblBaiViets.Where(b => b.MaTcn == trangcn.MaTcn).ToList();
                        var tcn = new TrangCaNhan
                        {
                            user = us,
                            TblTrangCn = trangcn,
                            BaiViet = baiviet
                        };
                        return View(tcn);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login","Acount");
        }
        public IActionResult Delete(int maBV)
        {
            int maTCN = HttpContext.Session.GetInt32("maTCN") ?? 0;
            var us1 = HttpContext.Session.GetString("UserName");
            var us2 = HttpContext.Session.GetString("UserName2");
            if(us1 == us2)
            {
                var baiViet = _context.TblBaiViets.Where(b=>b.MaBv==maBV).FirstOrDefault();
                if (baiViet != null)
                {
                    _context.TblBaiViets.Remove(baiViet);
                    _context.SaveChanges();
                    HttpContext.Session.Remove("maTCN");
                    return RedirectToAction("Index", new { maTCN = maTCN });
                }

            }
            return RedirectToAction("Index", new { maTCN = maTCN });
        }
    }
}
