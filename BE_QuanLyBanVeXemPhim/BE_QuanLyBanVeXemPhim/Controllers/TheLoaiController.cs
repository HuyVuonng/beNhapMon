using BE_QuanLyBanVeXemPhim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_QuanLyBanVeXemPhim.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class TheLoaiController : ControllerBase
    {
        private DbNhapMonContext _dB { get; set; }
        private readonly ILogger<TheLoaiController> _logger;
        private readonly IConfiguration _config;

        public TheLoaiController(DbNhapMonContext dbNhapMonContext, ILogger<TheLoaiController> logger, IConfiguration configuration)
        {
            this._dB = dbNhapMonContext;
            _logger = logger;
            _config = configuration;
        }


        [HttpGet]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAll() {
            List<TblTheLoai> theLoai = this._dB.GetAllTheLoai().ToList(); 

            return Ok(theLoai);
        }


        /// <summary>
        /// Chỉ cần truyền tên thể loại body chỉ có {"sTenTheLoai": "string"}
        /// </summary>

    [HttpPost]
        [Route("/Add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddTheLoai([FromBody] TblTheLoai tblTheLoai)
        {
            this._dB.addTheLoai(tblTheLoai.sTenTheLoai);
            List<TblTheLoai> theLoai = this._dB.GetAllTheLoai().ToList();
            return Ok(theLoai);
        }

        /// <summary>
        /// Chỉ cần truyền id thể loại body chỉ có {"pK_iTheLoaiID": id thể loại cần xoá}
        /// </summary>
        [HttpDelete]
        [Route("/Delete")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteTheLoai([FromBody] TblTheLoai tblTheLoai)
        {
            this._dB.deleteTheLoai((int)tblTheLoai.PK_iTheLoaiID);
            List<TblTheLoai> theLoai = this._dB.GetAllTheLoai().ToList();
            return Ok(theLoai);
        }
    }
}
