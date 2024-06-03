using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaiTapLonWNC.Models.Authen
{
    public class Authen:ActionFilterAttribute
    {
        private readonly string _loai;
        public Authen(string loai)
        {
            _loai = loai;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var username = session.GetString("UserName");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                // Người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập hoặc trả về lỗi 403
                context.Result = new RedirectToActionResult("Login", "Acount", null);
                return;
            }
            if (!string.Equals(role, _loai, StringComparison.OrdinalIgnoreCase))
            {
                // Người dùng không có quyền truy cập, trả về lỗi 403
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
        }
    }
}
