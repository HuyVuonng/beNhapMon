using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [Route("/registerAccountManager")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> registerAccountManager([FromBody] RegisterUserAccount data)
        {
            List<TblUser> user = this._dB.checkUserRegister(data.SUserName).ToList();
            if (user.Count > 0)
            {
                return StatusCode(400, "Đã tồn tại emial này");

            }
            this._dB.registerAccount(data.SUserName, data.SPassword, data.SFullName, data.DDateOfBirth, data.SPhoneNumber, "Manager");
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



        [HttpGet]
        [Route("/getInfor")]
		[Authorize]
		public async Task<IActionResult> getInfor()
        {
			var Role= User.FindFirstValue(ClaimTypes.Role);
			var userName = User.Claims.FirstOrDefault(x => x.Type.Equals("username", StringComparison.InvariantCultureIgnoreCase));
			var fullName=User.Claims.FirstOrDefault(x => x.Type.Equals("fullname", StringComparison.InvariantCultureIgnoreCase));
			var dateOfBirth = User.Claims.FirstOrDefault(x => x.Type.Equals("dateOfBirth", StringComparison.InvariantCultureIgnoreCase));
            var phoneNumber = User.Claims.FirstOrDefault(x => x.Type.Equals("phoneNumber", StringComparison.InvariantCultureIgnoreCase));
            return Ok(new {
				Role,
				userName= userName.Value,
                fullName= fullName.Value,
                dateOfBirth = dateOfBirth.Value,
                phoneNumber = phoneNumber.Value,
            });
           
        }
    }
}
