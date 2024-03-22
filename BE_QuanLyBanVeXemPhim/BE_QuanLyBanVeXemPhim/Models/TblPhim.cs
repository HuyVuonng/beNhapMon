using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblPhim
    {
        [Key]
        [AllowNull]
        public long? PK_iPhimID { get; set; }
        public string sTenPhim { get; set; } = null!;
        public long iThoiLuong { get; set; }
        public long FK_iTheLoaiID { get; set; }
        public string sAnhQuangCao { get; set; } = null!;
        public DateTime dNgayChieu { get; set; }
        public double fGia { get; set; }
        public string sNoiDung { get; set; } = null!;
        public string sTrailer { get; set; } = null!;
        public string sTrangThai { get; set; } = null!;
	}
}
