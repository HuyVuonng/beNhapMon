using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GioChieuController : ControllerBase
    {
        private DbNhapMonContext _dB { get; set; }
        private readonly ILogger<GioChieuController> _logger;
        private readonly IConfiguration _config;

        public GioChieuController(DbNhapMonContext dbNhapMonContext, ILogger<GioChieuController> logger, IConfiguration configuration)
        {
            this._dB = dbNhapMonContext;
            _logger = logger;
            _config = configuration;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllGioChieu()
        {
            List<TblGioChieu> listGioChieu = this._dB.TblGioChieu.ToList();

           
            return Ok(listGioChieu);
        }


        [HttpGet]
        [Route("getByID")]
        public async Task<IActionResult> GetGioChieuByID(int id)
        {
            var giochieu = this._dB.TblGioChieu.Where(g => g.PK_iGioChieuID == id).FirstOrDefault();
            if (giochieu == null)
            {
                return NotFound();
            }
            return Ok(giochieu);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddGioChieu([FromBody] AddGioChieu tblGioChieu)
        {
           var tblGioChieuAdd= new TblGioChieu();
            tblGioChieuAdd.sTenGio = tblGioChieu.sTenGio;
            tblGioChieuAdd.PK_iGioChieuID = null;
                this._dB.TblGioChieu.Add(tblGioChieuAdd);
                this._dB.SaveChanges();

            return Ok(this._dB.TblGioChieu.ToList());
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdatePhim([FromBody] TblGioChieu tblGioChieu)
        {
            if (ModelState.IsValid)
            {
                var gioChieuEdit = this._dB.TblGioChieu.Where(e => e.PK_iGioChieuID == tblGioChieu.PK_iGioChieuID).FirstOrDefault();
                if (gioChieuEdit != null)
                {
                    gioChieuEdit.sTenGio = tblGioChieu.sTenGio;
   
                    this._dB.SaveChanges();
                }
                else
                {
                    return NotFound("Không tồn tại giờ chiếu này");
                }
                return Ok(this._dB.TblGioChieu.Where(e => e.PK_iGioChieuID == tblGioChieu.PK_iGioChieuID).FirstOrDefault());
            }
            else
                return BadRequest("Điền đủ các trường");
        }


        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeletePhim([FromBody] DeleteGioChieu gioChieu)
        {
            var gioChieuDelete = this._dB.TblGioChieu.Where(e => e.PK_iGioChieuID == gioChieu.PK_iGioChieuID).FirstOrDefault();
            if (gioChieuDelete != null)
            {
                this._dB.TblGioChieu.Remove(gioChieuDelete);
                this._dB.SaveChanges(true);
            }
            return Ok();
        }

    }
}
