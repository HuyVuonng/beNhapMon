using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private DbNhapMonContext _dB { get; set; }
        private readonly ILogger<PhimController> _logger;
        private readonly IConfiguration _config;

        public PhimController(DbNhapMonContext dbNhapMonContext, ILogger<PhimController> logger, IConfiguration configuration)
        {
            this._dB = dbNhapMonContext;
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllPhim(int page = 1, int pageSize = 1)
        {
            List<TblPhim> listPhim = this._dB.TblPhim.ToList();

            var pageIndex = page;
            var totalPage = listPhim.Count;
            var numberPage = Math.Ceiling((float)(totalPage / pageSize));
            var start = (pageIndex - 1) * pageSize;
            var phim = listPhim.Skip(start).Take(pageSize);

            var json = new
            {
                data = phim,
                totalItem = listPhim.Count,
                numberPage,
                page,
                pageSize
            };
            return Ok(json);
        }


        [HttpGet]
        [Route("getByID")]
        public async Task<IActionResult> GetPhimByID(int id)
        {
            var phim = this._dB.TblPhim.Where(g => g.PK_iPhimID == id).FirstOrDefault();
            if (phim == null)
            {
                return NotFound();
            }
            return Ok(phim);
        }
      
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddPhim([FromBody] AddPhim tblPhim)
        {
            if (this._dB.TblTheLoai.Any(e => e.PK_iTheLoaiID == tblPhim.FK_iTheLoaiID))
            {
                var phimAdd = new TblPhim();
                phimAdd.sTenPhim=tblPhim.sTenPhim;
                phimAdd.fGia=tblPhim.fGia;
                phimAdd.iThoiLuong = tblPhim.iThoiLuong;
                phimAdd.dNgayChieu = tblPhim.dNgayChieu;
                phimAdd.FK_iTheLoaiID = tblPhim.FK_iTheLoaiID;
                phimAdd.sAnhQuangCao = tblPhim.sAnhQuangCao;
                phimAdd.sNoiDung=tblPhim.sNoiDung;
                phimAdd.sTrailer=tblPhim.sTrailer;
                phimAdd.sTrangThai=tblPhim.sTrangThai;
                phimAdd.PK_iPhimID = null;
                this._dB.TblPhim.Add(phimAdd);
                this._dB.SaveChanges();
            }
            else
            {
                return BadRequest("Không có thể loại phim này");
            }
           
            return Ok(this._dB.TblPhim.ToList());
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdatePhim([FromBody] TblPhim tblPhim)
        {
            if (ModelState.IsValid)
            {
                var phimEdit = this._dB.TblPhim.Where(e => e.PK_iPhimID == tblPhim.PK_iPhimID).FirstOrDefault();
                if (phimEdit != null)
                {
                    phimEdit.sTenPhim = tblPhim.sTenPhim;
                    phimEdit.iThoiLuong = tblPhim.iThoiLuong;
                    phimEdit.fGia = tblPhim.fGia;
                    phimEdit.dNgayChieu = tblPhim.dNgayChieu;
                    if(this._dB.TblTheLoai.Any(e=>e.PK_iTheLoaiID== tblPhim.FK_iTheLoaiID)){
                        phimEdit.FK_iTheLoaiID = tblPhim.FK_iTheLoaiID;
                    }
                    else
                    {
                        return BadRequest("Không có thể loại phim này");
                    }
                    phimEdit.sAnhQuangCao = tblPhim.sAnhQuangCao;
                    phimEdit.sNoiDung = tblPhim.sNoiDung;
                    phimEdit.sTrailer = tblPhim.sTrailer;
                    phimEdit.sTrangThai = tblPhim.sTrangThai;
                    this._dB.SaveChanges();
                }
                else
                {
                    return NotFound("Không tồn tại ghế này");
                }
                return Ok(this._dB.TblPhim.Where(e => e.PK_iPhimID == tblPhim.PK_iPhimID).FirstOrDefault());
            }
            else
                return BadRequest("Điền đủ các trường");
        }


        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeletePhim([FromBody] DeletePhim tblPhim)
        {
            var phimDelete = this._dB.TblPhim.Where(e => e.PK_iPhimID == tblPhim.PK_iPhimID).FirstOrDefault();
            if (phimDelete != null)
            {
                this._dB.TblPhim.Remove(phimDelete);
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
