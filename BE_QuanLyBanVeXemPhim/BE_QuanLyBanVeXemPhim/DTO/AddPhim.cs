namespace BE_QuanLyBanVeXemPhim.DTO
{
    public class AddPhim
    {
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
