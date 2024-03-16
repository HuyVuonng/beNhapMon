using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblPhong
    {
        [Key]
        public long PK_iPhongID { get; set; }
        public string sTenPhong { get; set; } = null!;
        public long iSoLuongGhe { get; set; }

    }
}
