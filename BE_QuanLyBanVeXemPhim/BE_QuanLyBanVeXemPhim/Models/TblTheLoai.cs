using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblTheLoai
    {
        [Key]
        public long PK_iTheLoaiID { get; set; }
        public string sTenTheLoai { get; set; } = null!;
    }
}
