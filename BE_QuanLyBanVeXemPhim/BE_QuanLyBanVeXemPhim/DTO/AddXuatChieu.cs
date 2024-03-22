namespace BE_QuanLyBanVeXemPhim.DTO
{
	public class AddXuatChieu
	{
		public long FK_iPhimID { get; set; }
		public long FK_iPhongID { get; set; }
		public long FK_iGioChieuID { get; set; }
		public DateTime dNgayChieu { get; set; }
	}
}
