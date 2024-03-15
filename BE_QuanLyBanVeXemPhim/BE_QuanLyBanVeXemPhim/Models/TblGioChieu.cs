using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblGioChieu
    {
        [Key]
        public long PK_iGioChieuID { get; set; }

        public string sTenGio { get; set; } = null!;
    }
}
