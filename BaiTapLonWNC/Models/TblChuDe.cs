using System;
using System.Collections.Generic;

namespace BaiTapLonWNC.Models;

public partial class TblChuDe
{
    public int MaChuDe { get; set; }

    public string TenChuDe { get; set; } = null!;

    public virtual ICollection<TblBaiViet> TblBaiViets { get; set; } = new List<TblBaiViet>();
}
