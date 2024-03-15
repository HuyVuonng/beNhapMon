using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BE_QuanLyBanVeXemPhim.Models;

public partial class TblUser
{
    [Key]
    public string SUserName { get; set; } = null!;

    public string SFullName { get; set; } = null!;

    public string SPhoneNumber { get; set; } = null!;

    public string SPassword { get; set; } = null!;

    public DateOnly DDateOfBirth { get; set; }

    public string SRole { get; set; } = null!;
}
