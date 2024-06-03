using BaiTapLonWNC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using BaiTapLonWNC.Models.Authen;

namespace BaiTapLonWNC.Controllers
{
    [Authen("True")]
    public class AdminController : Controller
    {
        private readonly BaiTapLonWebContext _db;
        public AdminController(BaiTapLonWebContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult dsUser()
        {
            try
            {
                var user = _db.TblUsers.ToList();

                return Json(new {code=200 ,dsus=user,msg="Lấy dữ liệu thành công"});
            }
            catch (Exception ex)
            {
                return Json(new { code = 500,msg = "Lấy dữ liệu thất bại:"+ex.Message });
            }
        }
        [HttpPost]
        public JsonResult addUser(string username, string pass, string name,bool loaius)
        {
            if (username != null && pass != null && name != null)
            {
                var u = _db.TblUsers.Where(u => u.UserName.Equals(username)).FirstOrDefault();
                if (u == null)
                {
                    var u1 = new TblUser
                    {
                        UserName = username,
                        Pass = pass,
                        LoaiUser=loaius
                    };
                    _db.TblUsers.Add(u1);
                    _db.SaveChanges();
                    var us = _db.TblUsers.Where(u => u.UserName.Equals(username)).FirstOrDefault();
                    if (us != null)
                    {
                        var tcn = new TblTrangCn
                        {
                            NameTcn = name,
                            MaUs = us.MaUs
                        };
                        _db.TblTrangCns.Add(tcn);
                        _db.SaveChanges();
                        return Json(new { code = 200, msg = "Thêm mới thành công" });
                    }
                    else
                    {
                        return Json(new { code = 500, msg = "User này không tồn tại" });
                    }
                }
                else
                {

                    return Json(new { code = 501, msg = "Tài khoản đã tồn tại" });
                }
            }
            return Json(new { code = 502, msg = "Vui lòng nhập đủ dữ liệu" });
        }
        [HttpGet]
        public JsonResult TrangCn(int id)
        {
            if (id!=0)
            {
                var tcn = _db.TblTrangCns.Where(t=>t.MaUs==id).FirstOrDefault();
                return Json(new { code = 200,tcn=tcn,msg="thành công"});
            }
            else
            {
                return Json(new { code = 500, msg = "thất bại" });
            }
        }
        [HttpGet]
        public JsonResult user(int id)
        {
            if (id != 0)
            {
                var us = _db.TblUsers.Where(t => t.MaUs == id).FirstOrDefault();
                return Json(new { code = 200, us = us, msg = "thành công" });
            }
            else
            {
                return Json(new { code = 500, msg = "thất bại" });
            }
        }
        [HttpPost]
        public JsonResult UpdateUser(int id, string username, string pass, string name, bool loaius)
        {
            if (username != null && pass != null && name != null && id!=0)
            {
                var u= _db.TblUsers.Where(u => u.MaUs==id).FirstOrDefault();
                if (u != null)
                {
                    u.UserName = username;
                    u.Pass = pass;
                    u.LoaiUser = loaius;
                    _db.TblUsers.Update(u);
                    _db.SaveChanges();
                    var tcn = _db.TblTrangCns.Where(s => s.MaUs == id).FirstOrDefault();
                    tcn.NameTcn = name;
                    _db.TblTrangCns.Update(tcn);
                    _db.SaveChanges();
                    return Json(new { code = 200, msg = "Cập nhật thành công" });
                }
                else
                {

                    return Json(new { code = 501, msg = "Tài khoản không tồn tại" });
                }
            }
            return Json(new { code = 502, msg = "Vui lòng nhập đủ dữ liệu" });
        }
        [HttpPost]
        public JsonResult xoa(int id) {
            try
            {
                var us = _db.TblUsers.Where(u => u.MaUs == id).FirstOrDefault();
                var tcn = _db.TblTrangCns.Where(t => t.MaUs == id).FirstOrDefault();
                var baiViet = _db.TblBaiViets.Where(b => b.MaTcn == tcn.MaTcn).ToList();
                _db.TblBaiViets.RemoveRange(baiViet);
                _db.SaveChanges();
                _db.TblTrangCns.RemoveRange(tcn); _db.SaveChanges();
                _db.TblUsers.Remove(us);
                _db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 501, msg = "Xóa thất bại "+ex.Message });
            }
        }
        public IActionResult Post()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DSPost()
        {
            try
            {
                var dsp = _db.TblBaiViets.ToList();

                return Json(new { code = 200, dsp = dsp, msg = "Lấy dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu thất bại:" + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult ChuDe()
        {
            try
            {
                var dscd = _db.TblChuDes.ToList();

                return Json(new { code = 200, dscd = dscd, msg = "Lấy dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu thất bại:" + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult trangcanhan()
        {
            try
            {
                var dscn = _db.TblTrangCns.ToList();

                return Json(new { code = 200, dscn = dscn, msg = "Lấy dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu thất bại:" + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult postAnh(string tieude, string noidung, IFormFile file, int chude, int trangcn)
        {
            try
            {

                //if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                //{
                var finame = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", finame);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    var baiviet = new TblBaiViet
                    {
                        TieuDe=tieude,
                        NoiDung=noidung,
                        LinkAnh=finame,
                        MaChuDe=chude,
                        MaTcn=trangcn
                    };
                    _db.TblBaiViets.Add(baiviet);
                    _db.SaveChanges();
                }

                return Json(new { code = 200, msg = "thêm thành công" });
                //}
                //else
                //{
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm thất bại:"+ ex.Message });
            }
            //}
        }
        [HttpGet]
        public JsonResult TrangCn1(int id)
        {
            if (id != 0)
            {
                var tcn = _db.TblTrangCns.Where(t => t.MaTcn == id).FirstOrDefault();
                return Json(new { code = 200, tcn = tcn, msg = "thành công" });
            }
            else
            {
                return Json(new { code = 500, msg = "thất bại" });
            }
        }
        [HttpPost]
        public JsonResult UpdateBV(int maBv, string tieude, string noidung, IFormFile file, int chude, int trangcn)
        {
            try
            {
                var baiviet = _db.TblBaiViets.Where(b=>b.MaBv== maBv).FirstOrDefault();
                //if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                //{
                if (baiviet != null)
                {
                    var finame = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", finame);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);


                        baiviet.TieuDe = tieude;
                        baiviet.NoiDung = noidung;
                        baiviet.LinkAnh = finame;
                        baiviet.MaChuDe = chude;
                        baiviet.MaTcn = trangcn;

                        _db.TblBaiViets.Update(baiviet);
                        _db.SaveChanges();
                    }

                    return Json(new { code = 200, msg = "cập nhật thành công" });
                    //}
                    //else
                    //{
                }
                else
                {
                    return Json(new { code = 500, msg = "Bài viết không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 501, msg = "Thêm thất bại:" + ex.Message });
            }
            //}
        }
        [HttpGet]
        public JsonResult BaiViet1(int id)
        {
            try
            {
                var baiviet = _db.TblBaiViets.Where(b => b.MaBv == id).FirstOrDefault();
                return Json(new { code = 200, baiviet = baiviet, msg = "thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu thất bại" + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult XoaBV(int id)
        {
            try
            {
                var baiViet = _db.TblBaiViets.Where(b => b.MaBv == id).FirstOrDefault();
                _db.TblBaiViets.Remove(baiViet);
                _db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 501, msg = "Xóa thất bại " + ex.Message });
            }
        }
        public IActionResult DSChuDe()
        {
            return View();
        }
        [HttpPost]
        public JsonResult addCD(string chude)
        {
            try
            {
                var cd = new TblChuDe
                {
                    TenChuDe = chude
                };
                _db.TblChuDes.Add(cd);
                _db.SaveChanges();
                return Json(new { code = 200, msg = "thêm mới thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "thêm mới thất bại " + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult chude1(int id)
        {
            try
            {
                var chude = _db.TblChuDes.Where(c => c.MaChuDe == id).FirstOrDefault();
                return Json(new { code = 200, chude=chude, msg = "lấy thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "lấy thất bại " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateChuDe(int id, string chude)
        {
            try
            {
                var cd = _db.TblChuDes.Where(c => c.MaChuDe == id).FirstOrDefault();
                if(cd != null) {
                    cd.TenChuDe = chude;

                    _db.TblChuDes.Update(cd);
                    _db.SaveChanges();
                    return Json(new { code = 200, msg = "Cập nhật thành công" });
                }
                else
                {
                    return Json(new { code = 501, msg = "Chủ đề không tồn tại" });
                }
                 
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thất bại " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult xoacd(int id)
        {
            try
            {
                var baiViet = _db.TblBaiViets.Where(b => b.MaChuDe == id).ToList();
                _db.TblBaiViets.RemoveRange(baiViet);
                _db.SaveChanges();
                var cd = _db.TblChuDes.Where(c=>c.MaChuDe ==id).FirstOrDefault();
                _db.TblChuDes.Remove(cd);
                _db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 501, msg = "Xóa thất bại " + ex.Message });
            }
        }
    }
}
