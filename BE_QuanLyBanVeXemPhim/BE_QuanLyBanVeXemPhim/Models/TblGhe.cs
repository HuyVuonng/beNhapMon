using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BE_QuanLyBanVeXemPhim.Models
{
    public partial class TblGhe
    {
        [Key]
        [AllowNull]
        public long? PK_iGheID { get; set; }

        public string sTenGhe { get; set; } = null!;
    }
}
