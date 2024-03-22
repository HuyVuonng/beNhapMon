using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblPhong
    {
        [Key]
        [AllowNull]
        public long? PK_iPhongID { get; set; }
        public string sTenPhong { get; set; } = null!;
        public long iSoLuongGhe { get; set; }

    }
}
