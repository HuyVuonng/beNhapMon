using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class XuatChieuController : ControllerBase
	{
		private DbNhapMonContext _dB { get; set; }
		private readonly ILogger<XuatChieuController> _logger;
		private readonly IConfiguration _config;

		public XuatChieuController(DbNhapMonContext dbNhapMonContext, ILogger<XuatChieuController> logger, IConfiguration configuration)
		{
			this._dB = dbNhapMonContext;
			_logger = logger;
			_config = configuration;
		}
		[HttpGet]
		[Route("getAll")]
		public async Task<IActionResult> GetAllXuatChieu()
		{
			List<TblXuatChieu> listXuatChieu = this._dB.TblXuatChieu.ToList();

			return Ok(listXuatChieu);
		}
		[HttpGet]
		[Route("getByID")]
		public async Task<IActionResult> GetXuatChieuByID(int id)
		{
			var xuatChieu = this._dB.TblXuatChieu.Where(g => g.PK_iXuatChieuID == id).FirstOrDefault();
			if (xuatChieu == null)
			{
				return NotFound();
			}
			return Ok(xuatChieu);
		}

		[HttpGet]
		[Route("getByPhimID")]
		public async Task<IActionResult> GetXuatChieuByPhimID(int id)
		{
			var xuatChieu = this._dB.TblXuatChieu.Where(g => g.FK_iPhimID == id).FirstOrDefault();
			if (xuatChieu == null)
			{
				return NotFound();
			}
			return Ok(xuatChieu);
		}

		[HttpPost]
		[Route("Add")]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> AddXuatChieu([FromBody] AddXuatChieu xuatchieu)
		{
			var xuatchieuAdd = new TblXuatChieu();
			xuatchieuAdd.FK_iPhimID = xuatchieu.FK_iPhimID;
			xuatchieuAdd.FK_iPhongID = xuatchieu.FK_iPhongID;
			xuatchieuAdd.FK_iGioChieuID = xuatchieu.FK_iGioChieuID;
			xuatchieuAdd.dNgayChieu = xuatchieu.dNgayChieu;
			xuatchieuAdd.PK_iXuatChieuID = null;
			this._dB.TblXuatChieu.Add(xuatchieuAdd);
			this._dB.SaveChanges();
			return Ok(this._dB.TblXuatChieu.ToList());
		}

		[HttpPut]
		[Route("Update")]
		[Authorize(Roles = "Admin,Manager")]
		public async Task<IActionResult> UpdateXuatChieu([FromBody] TblXuatChieu tblXuatChieu)
		{
			if (ModelState.IsValid)
			{
				var xuatChieuEdit = this._dB.TblXuatChieu.Where(e => e.PK_iXuatChieuID == tblXuatChieu.PK_iXuatChieuID).FirstOrDefault();
				if (xuatChieuEdit != null)
				{
					xuatChieuEdit.FK_iPhimID = tblXuatChieu.FK_iPhimID;
					xuatChieuEdit.FK_iPhongID = tblXuatChieu.FK_iPhongID;
					xuatChieuEdit.FK_iGioChieuID = tblXuatChieu.FK_iGioChieuID;
					xuatChieuEdit.dNgayChieu = tblXuatChieu.dNgayChieu;
					this._dB.SaveChanges();
				}
				else
				{
					return NotFound("Không tồn tại xuất chiếu này");
				}
				return Ok(this._dB.TblXuatChieu.Where(e => e.PK_iXuatChieuID == tblXuatChieu.PK_iXuatChieuID).FirstOrDefault());
			}
			else
				return BadRequest("Điền đủ các trường");
		}



		[HttpDelete]
		[Route("Delete")]
		[Authorize(Roles = "Admin,Manager")]
		public async Task<IActionResult> DeleteGhe([FromBody] DeleteXuatChieu xuatChieu)
		{
			var xuatChieuDelete = this._dB.TblXuatChieu.Where(e => e.PK_iXuatChieuID == xuatChieu.PK_iXuatChieuID).FirstOrDefault();
			if (xuatChieuDelete != null)
			{
				this._dB.TblXuatChieu.Remove(xuatChieuDelete);
				this._dB.SaveChanges(true);
			}
			else
			{
				return NotFound();
			}
			return Ok();
		}


	}
}
