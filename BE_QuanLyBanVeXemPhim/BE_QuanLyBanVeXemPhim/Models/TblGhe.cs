using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblGhe
    {
        [Key]
        public long PK_iGheID { get; set; }

        public long iSoghe { get; set; }
    }
}
