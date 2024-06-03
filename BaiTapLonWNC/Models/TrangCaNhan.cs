namespace BaiTapLonWNC.Models
{
    public class TrangCaNhan
    {
        public TblUser user { get; set; }
        public TblTrangCn TblTrangCn { get; set; }
        public List<TblBaiViet> BaiViet { get; set;}
    }
}
