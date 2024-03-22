using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BE_QuanLyBanVeXemPhim.Models
{
	public partial class TblXuatChieu
	{
		[Key]
		[AllowNull]
		public long? PK_iXuatChieuID { get; set; }
		public long FK_iPhimID { get; set; }
		public long FK_iPhongID { get; set; }
		public long FK_iGioChieuID { get; set; }
		public DateTime dNgayChieu { get; set; }
	}
}
