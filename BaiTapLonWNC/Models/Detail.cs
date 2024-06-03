namespace BaiTapLonWNC.Models
{
    public class Detail
    {
        public TblBaiViet BaiViet { get; set; }
        public TblTrangCn TrangCn { get; set; }

        public List<TblLike> Likes { get; set; }

        public List<TblComment> Comments { get; set; }
        public List<TblTrangCn> nameCn { get; set; }
        public TblLike like { get; set; }

        public TblComment comment { get; set; }

    }
}
