using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblGioChieu
    {
        [Key]
        [AllowNull]
        public long? PK_iGioChieuID { get; set;}

        public string sTenGio { get; set; } = null!;
	}
}
