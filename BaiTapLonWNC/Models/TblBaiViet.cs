using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaiTapLonWNC.Models;

public partial class TblBaiViet
{
    public int MaBv { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string? LinkAnh { get; set; }

    public int? SoLike { get; set; }

    public int? SoComment { get; set; }

    public int MaChuDe { get; set; }

    public int MaTcn { get; set; }
    [MaxLength(10,ErrorMessage ="bạn chỉ được nhập 10 ký tự")]
    [MinLength(10,ErrorMessage ="bạn chỉ được nhập 10 ký tự")]
    public string? VerifyKey { get; set; }

    public virtual TblChuDe MaChuDeNavigation { get; set; } = null!;

    public virtual TblTrangCn MaTcnNavigation { get; set; } = null!;

    public virtual ICollection<TblComment> TblComments { get; set; } = new List<TblComment>();

    public virtual ICollection<TblLike> TblLikes { get; set; } = new List<TblLike>();
}
