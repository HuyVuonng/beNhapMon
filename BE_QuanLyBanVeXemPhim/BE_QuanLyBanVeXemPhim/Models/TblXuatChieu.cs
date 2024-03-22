using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
	public partial class TblXuatChieu
	{
		[Key]
		public long PK_iXuatChieuID { get; set; }
		public long FK_iPhimID { get; set; }
		public long FK_iPhongID { get; set; }
		public long FK_iGioChieuID { get; set; }
		public DateTime dNgayChieu { get; set; }
	}
}
