using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private DbNhapMonContext _dB { get; set; }
		private readonly ILogger<AccountController> _logger;
		private readonly IConfiguration _config;

		public AccountController(DbNhapMonContext dbNhapMonContext, ILogger<AccountController> logger, IConfiguration configuration)
		{
			this._dB = dbNhapMonContext;
			_logger = logger;
			_config = configuration;
		}


		[HttpPost]
		[Route("/registerAccountUser")]
		public async Task<IActionResult> registerAccountUser([FromBody] RegisterUserAccount data)
		{
			List<TblUser> user = this._dB.checkUserRegister(data.SUserName).ToList();
			if (user.Count>0) {
				return StatusCode(400, "Đã tồn tại userName này");

			}
			this._dB.registerAccount(data.SUserName,data.SPassword, data.SFullName,data.DDateOfBirth,data.SPhoneNumber,"User");
			return Ok();
		}


		[HttpPost]
		[Route("/registerAccountEmployee")]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> registerAccountEmployee([FromBody] RegisterUserAccount data)
		{
			List<TblUser> user = this._dB.checkUserRegister(data.SUserName).ToList();
			if (user.Count > 0)
			{
				return StatusCode(400, "Đã tồn tại emial này");

			}
			this._dB.registerAccount(data.SUserName, data.SPassword, data.SFullName, data.DDateOfBirth, data.SPhoneNumber, "Employee");
			return Ok();
		}


		[HttpPost]
		[Route("/loginAccount")]
		public async Task<IActionResult> loginAccount([FromBody] loginAccount data)
		{
			List<TblUser> users = this._dB.loginAccount(data.SUserName, data.SPassword).ToList();

			if (users.Count>0) {
				TblUser user = users.First();

				var claims = new[]
			   {
				new Claim("fullName",user.SFullName),
				new Claim("userName",user.SUserName),
				new Claim("dateOfBirth",user.DDateOfBirth.ToString()),
				new Claim("phoneNumber",user.SPhoneNumber),
				new Claim("Role",user.SRole),
				new Claim(ClaimTypes.Role,user.SRole),
            };
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken(_config["Tokens:Issuer"],
					_config["Tokens:Issuer"],
					claims,
					expires: DateTime.Now.AddMinutes(30),
					signingCredentials: creds);


				return Ok(new JwtSecurityTokenHandler().WriteToken(token));
			}
			return StatusCode(400, "Không có user này");
		}



        [HttpPost]
        [Route("/getRole")]
        [Authorize]
        public async Task<IActionResult> getRole([FromBody] loginAccount data)
        {
            List<TblUser> users = this._dB.loginAccount(data.SUserName, data.SPassword).ToList();

            if (users.Count > 0)
            {
                TblUser user = users.First();
               
                return Ok(user.SRole);
            }
            return StatusCode(400, "Không có user này");
        }
    }
}
