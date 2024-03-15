namespace BE_QuanLyBanVeXemPhim.DTO
{
	public class RegisterUserAccount
	{
		public string SUserName { get; set; } = null!;

		public string SFullName { get; set; } = null!;

		public string SPhoneNumber { get; set; } = null!;

		public string SPassword { get; set; } = null!;

		public string DDateOfBirth { get; set; }
	}
}
