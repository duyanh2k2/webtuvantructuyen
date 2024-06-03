using System;
using System.Collections.Generic;

namespace BaiTapLonWNC.Models;

public partial class TblLike
{
    public int MaLike { get; set; }

    public string Icon { get; set; } = null!;

    public int MaTcn { get; set; }

    public int MaBv { get; set; }

    public virtual TblBaiViet MaBvNavigation { get; set; } = null!;

    public virtual TblTrangCn MaTcnNavigation { get; set; } = null!;
}
