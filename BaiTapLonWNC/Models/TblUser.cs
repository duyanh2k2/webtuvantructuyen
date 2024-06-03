using System;
using System.Collections.Generic;

namespace BaiTapLonWNC.Models;

public partial class TblUser
{
    public int MaUs { get; set; }

    public string UserName { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public bool LoaiUser { get; set; }

    public virtual ICollection<TblTrangCn> TblTrangCns { get; set; } = new List<TblTrangCn>();
}
