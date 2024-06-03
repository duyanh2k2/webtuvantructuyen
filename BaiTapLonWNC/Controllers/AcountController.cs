using BaiTapLonWNC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapLonWNC.Controllers
{
    public class AcountController : Controller
    {
        private readonly BaiTapLonWebContext db;
        public AcountController(BaiTapLonWebContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(TblUser user) 
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    var u = db.TblUsers.Where(u => u.UserName.Equals(user.UserName) && u.Pass.Equals(user.Pass)).FirstOrDefault();
                    if (u != null)
                    {
                        HttpContext.Session.SetString("UserName", u.UserName.ToString());
                        var tcn = db.TblTrangCns.Where(t => t.MaUs == u.MaUs).FirstOrDefault();
                        HttpContext.Session.SetString("Name", tcn.NameTcn.ToString());
                        HttpContext.Session.SetInt32("IdCaNhan", tcn.MaTcn);
                        HttpContext.Session.SetString("Role", u.LoaiUser.ToString());
                        if (u.LoaiUser == false)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.TaiKhoan = "Tài Khoản mật khẩu không chính xác";
                        return View();
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("IdCaNhan");
            HttpContext.Session.Remove("Role");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(TblUser user,TblTrangCn trangCn)
        {
            if (user.UserName!=null && user.Pass!=null && trangCn.NameTcn!=null)
            {
                var u = db.TblUsers.Where(u => u.UserName.Equals(user.UserName)).FirstOrDefault();
                if (u == null)
                { 
                    db.TblUsers.Add(user);
                    db.SaveChanges();
                    var us = db.TblUsers.Where(u => u.UserName.Equals(user.UserName)).FirstOrDefault();
                    if (us != null)
                    {
                        trangCn.MaUs = us.MaUs;
                        db.TblTrangCns.Add(trangCn);
                        db.SaveChanges();
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.User = "Tài khoản không tồn tại để tạo trang cá nhân";
                        return View();
                    }
                }
                else
                {
                    ViewBag.User = "Tài khoản đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        //[HttpGet]
        //public IActionResult regTCN()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult regTCN(TblTrangCn trangCn)
        //{

        //    return View();
        //}
    }
}
