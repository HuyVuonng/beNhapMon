using BE_QuanLyBanVeXemPhim.DTO;
using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GheController : ControllerBase
    {
        private DbNhapMonContext _dB { get; set; }
        private readonly ILogger<GheController> _logger;
        private readonly IConfiguration _config;

        public GheController(DbNhapMonContext dbNhapMonContext, ILogger<GheController> logger, IConfiguration configuration)
        {
            this._dB = dbNhapMonContext;
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllGhe()
        {
            List<TblGhe> listGhe = this._dB.TblGhe.ToList();

            return Ok(listGhe);
        }
        [HttpGet]
        [Route("getByID")]
        public async Task<IActionResult> GetGheByID(int id)
        {
            var ghe = this._dB.TblGhe.Where(g=>g.PK_iGheID==id).FirstOrDefault();
            if(ghe == null)
            {
                return NotFound();
            }
            return Ok(ghe);
        }
        
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddGhe([FromBody] AddGhe tblGhe)
        {
            var tblGheAdd = new TblGhe();
            tblGheAdd.sTenGhe = tblGhe.sTenGhe;
            tblGheAdd.PK_iGheID = null;
            this._dB.TblGhe.Add(tblGheAdd);
            this._dB.SaveChanges();
            return Ok(this._dB.TblGhe.ToList());
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateGhe([FromBody] TblGhe tblGhe)
        {
            if (ModelState.IsValid)
            {
                var gheEdit = this._dB.TblGhe.Where(e => e.PK_iGheID == tblGhe.PK_iGheID).FirstOrDefault();
                if (gheEdit != null)
                {
                    gheEdit.sTenGhe = tblGhe.sTenGhe;
                    this._dB.SaveChanges();
                }
                else
                {
                    return NotFound("Không tồn tại ghế này");
                }
                return Ok(this._dB.TblGhe.Where(e => e.PK_iGheID == tblGhe.PK_iGheID).FirstOrDefault());
            }
            else
                return BadRequest("Điền đủ các trường");
        }


        
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteGhe([FromBody] DeleteGhe tblGhe)
        {
            var gheDelete = this._dB.TblGhe.Where(e => e.PK_iGheID == tblGhe.PK_iGheID).FirstOrDefault();
            if (gheDelete != null)
            {
                this._dB.TblGhe.Remove(gheDelete);
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
