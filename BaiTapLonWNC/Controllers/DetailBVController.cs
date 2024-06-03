using BaiTapLonWNC.Models;
using BaiTapLonWNC.Models.Authen;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapLonWNC.Controllers
{
    [Authen("False")]
    public class DetailBVController : Controller
    {
        private readonly BaiTapLonWebContext _context;
        public DetailBVController(BaiTapLonWebContext context)
        {
            _context = context;
        }

        public IActionResult Index(int maBV)
        {
            HttpContext.Session.SetInt32("maBV", maBV);
            var user = HttpContext.Session.GetString("UserName");
            if (user != null)
            {
                var baiViet = _context.TblBaiViets.Where(ma => ma.MaBv == maBV).FirstOrDefault();
                if (baiViet != null)
                {
                    var trangcn = _context.TblTrangCns.Where(m => m.MaTcn == baiViet.MaTcn).FirstOrDefault();
                    var like = _context.TblLikes.Where(m => m.MaBv == baiViet.MaBv).ToList();
                    var comment = _context.TblComments.Where(m => m.MaBv == baiViet.MaBv).ToList();
                    var namecn = _context.TblTrangCns.ToList();
                    var us = _context.TblUsers.Where(ma => ma.UserName.Equals(user)).FirstOrDefault();
                    var t = _context.TblTrangCns.Where(t => t.MaUs == us.MaUs).FirstOrDefault();
                    var tontai = _context.TblLikes.Where(l => l.MaTcn == t.MaTcn && l.MaBv == maBV).FirstOrDefault();
                    var detail = new Detail
                    {
                        BaiViet = baiViet,
                        TrangCn = trangcn,
                        Likes = like,
                        Comments = comment,
                        nameCn = namecn,
                        like = tontai
                    };
                    
                    return View(detail);
                }
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Login", "Acount");
        }

        [HttpPost]
        public IActionResult like(TblLike like)
        {
            if (like.Icon != null)
            {
                
                int maBV = HttpContext.Session.GetInt32("maBV") ?? 0;
                var user = HttpContext.Session.GetString("UserName");
                if (user != null)
                {
                    var us = _context.TblUsers.Where(ma => ma.UserName.Equals(user)).FirstOrDefault();
                    if(us != null)
                    {
                        like.MaBv = maBV;
                        var t = _context.TblTrangCns.Where(t=>t.MaUs==us.MaUs).FirstOrDefault();
                        if (t != null)
                        {
                            var tontai = _context.TblLikes.Where(l => l.MaTcn == t.MaTcn && l.MaBv == maBV).FirstOrDefault();
                            if (tontai == null)
                            {
                                like.MaTcn = t.MaTcn;
                                _context.TblLikes.Add(like);
                                _context.SaveChanges();
                               
                                return RedirectToAction("Index", new { maBV = maBV });
                            }
                            else
                            {
                                _context.TblLikes.Remove(tontai);
                                _context.SaveChanges();
                                
                                return RedirectToAction("Index", new { maBV = maBV });
                            }
                        }
                    }
                }

            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public IActionResult BinhLuan(TblComment comment)
        {
            if(comment.NoiDung != null)
            {
                int maBV = HttpContext.Session.GetInt32("maBV") ?? 0;
                var user = HttpContext.Session.GetString("UserName");
                if (user != null)
                {
                    var us = _context.TblUsers.Where(ma => ma.UserName.Equals(user)).FirstOrDefault();
                    if (us != null)
                    {
                        comment.MaBv = maBV;
                        var t = _context.TblTrangCns.Where(t => t.MaUs == us.MaUs).FirstOrDefault();
                        if (t != null)
                        {
                                comment.MaTcn = t.MaTcn;
                                _context.TblComments.Add(comment);
                                _context.SaveChanges();
                                return RedirectToAction("Index", new { maBV = maBV });
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
