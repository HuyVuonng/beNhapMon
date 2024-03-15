using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblPhim
    {
        [Key]
        public long PK_iPhimID { get; set; }
        public string sTenPhim { get; set; } = null!;
        public long iThoiLuong { get; set; }
        public long FK_iTheLoaiID { get; set; }
        public string sAnhQuangCao { get; set; } = null!;
        public DateTime dNgayChieu { get; set; }
        public float fGia { get; set; }
        public string sNoiDung { get; set; } = null!;
        public string sTrailer { get; set; } = null!;

        public virtual TblTheLoai TheLoai { get; set; } = null!;
    }
}
