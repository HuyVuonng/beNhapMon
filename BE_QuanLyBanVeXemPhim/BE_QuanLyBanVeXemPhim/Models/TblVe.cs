using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_QuanLyBanVeXemPhim.Models
{
    
    public partial class TblVe
    {
        [Key]
        public long FK_iPhimID { get; set; }
        public string FK_sUserName { get; set; } = null!;
        public long FK_iPhongID { get; set; }
        public long FK_iGheID { get; set; }
        public long FK_iGioChieuID { get; set; }
        public DateTime dThoiGianMua { get; set; }
		public long FK_iXuatChieuID { get; set; }

	}
}
