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
        [Route("/GetAllPhong")]
        public async Task<IActionResult> GetAll()
        {
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();

            return Ok(phong);
        }

        [HttpGet]
        [Route("/GetPhongByID")]
        public async Task<IActionResult> GetPhongByID(int id)
        {
            List<TblPhong> phong = this._dB.GetPhongByID(id).ToList();

            return Ok(phong);
        }

        /// <summary>
        /// Chỉ cần truyền tên phòng và số lượng ghế body chỉ có {"sTenphong": "string",iSoLuongGhe: int}
        /// </summary>

        [HttpPost]
        [Route("/AddPhong")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddPhong([FromBody] TblPhong tblPhong)
        {
            this._dB.addPhong(tblPhong.sTenPhong, (int)tblPhong.iSoLuongGhe);
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();
            return Ok(phong);
        }

        [HttpPut]
        [Route("/UpdatePhong")]
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


        /// <summary>
        /// Chỉ cần truyền id phong body chỉ có {"pK_iPhongID": id thể loại cần xoá}
        /// </summary>
        [HttpDelete]
        [Route("/DeletePhong")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeletePhong([FromBody] TblPhong tblPhong)
        {
            this._dB.deletePhong((int)tblPhong.PK_iPhongID);
            List<TblPhong> phong = this._dB.GetAllPhong().ToList();
            return Ok(phong);
        }
    }
}
