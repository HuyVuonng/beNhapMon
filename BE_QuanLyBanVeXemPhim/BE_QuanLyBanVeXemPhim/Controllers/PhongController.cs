using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private DbNhapMonContext _dB { get; set; }
        private readonly ILogger<PhongController> _logger;
        private readonly IConfiguration _config;

        public PhongController(DbNhapMonContext dbNhapMonContext, ILogger<PhongController> logger, IConfiguration configuration)
        {
            this._dB = dbNhapMonContext;
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();

            return Ok(phong);
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetPhongByID(int id)
        {
            List<TblPhong> phong = this._dB.GetPhongByID(id).ToList();

            return Ok(phong);
        }

       

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddPhong([FromBody] AddPhong tblPhong)
        {
            this._dB.addPhong(tblPhong.sTenPhong, (int)tblPhong.iSoLuongGhe);
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();
            return Ok(phong);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdatePhong([FromBody] TblPhong tblPhong)
        {
             if(ModelState.IsValid)
            {
                var phongedit = this._dB.TblPhong.Where(e => e.PK_iPhongID == tblPhong.PK_iPhongID).FirstOrDefault();
                if (phongedit != null)
                {
                    phongedit.iSoLuongGhe=tblPhong.iSoLuongGhe;
                    phongedit.sTenPhong = tblPhong.sTenPhong;
                    this._dB.SaveChanges();
                }
                else
                {
                    return NotFound("Không tồn tại phòng này");
                }
                return Ok(this._dB.TblPhong.Where(e => e.PK_iPhongID == tblPhong.PK_iPhongID).FirstOrDefault());
            }else
            return BadRequest("Điền đủ các trường");
        }


       
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeletePhong([FromBody] DeletePhong tblPhong)
        {
            this._dB.deletePhong((int)tblPhong.PK_iPhongID);
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();
            return Ok(phong);
        }
    }
}
