using System;
using System.Collections.Generic;

namespace BaiTapLonWNC.Models;

public partial class TblTrangCn
{
    public int MaTcn { get; set; }

    public string NameTcn { get; set; } = null!;

    public int MaUs { get; set; }

    public virtual TblUser MaUsNavigation { get; set; } = null!;

    public virtual ICollection<TblBaiViet> TblBaiViets { get; set; } = new List<TblBaiViet>();

    public virtual ICollection<TblComment> TblComments { get; set; } = new List<TblComment>();

    public virtual ICollection<TblLike> TblLikes { get; set; } = new List<TblLike>();
}
