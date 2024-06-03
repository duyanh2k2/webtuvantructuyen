namespace BaiTapLonWNC.Models
{
    public class Home
    {
        public TblBaiViet BaiViet { get; set; }
        public List<TblBaiViet> BaiViets { get; set; }
        public List<TblTrangCn> trangCns { get; set; }
        public List<TblChuDe> chuDes { get; set; }
        
    }
}
