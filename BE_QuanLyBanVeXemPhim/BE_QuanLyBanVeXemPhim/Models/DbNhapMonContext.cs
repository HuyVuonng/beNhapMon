using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BE_QuanLyBanVeXemPhim.Models;

public partial class DbNhapMonContext : DbContext
{
    public DbNhapMonContext()
    {
    }

    public DbNhapMonContext(DbContextOptions<DbNhapMonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUser> TblUsers { get; set; }
    public virtual DbSet<TblGhe> TblGhe { get; set; }
    public virtual DbSet<TblGioChieu> TblGioChieu { get; set; }
    public virtual DbSet<TblPhim> TblPhim { get; set; }
    public virtual DbSet<TblPhong> TblPhong { get; set; }
    public virtual DbSet<TblTheLoai> TblTheLoai { get; set; }
    public virtual DbSet<TblVe> TblVe { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=DB_Back_End.mssql.somee.com;packet size=4096;user id=Huyvuong_SQLLogin_1;pwd=x76x6zwr4y;data source=DB_Back_End.mssql.somee.com;persist security info=False;initial catalog=DB_Back_End;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.SUserName);

            entity.ToTable("tblUser");

            entity.Property(e => e.SUserName)
                .HasMaxLength(50)
                .HasColumnName("sUserName");
            entity.Property(e => e.DDateOfBirth).HasColumnName("dDateOfBirth");
            entity.Property(e => e.SFullName)
                .HasMaxLength(50)
                .HasColumnName("sFullName");
            entity.Property(e => e.SPassword)
                .HasMaxLength(50)
                .HasColumnName("sPassword");
            entity.Property(e => e.SPhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("sPhoneNumber");
            entity.Property(e => e.SRole)
                .HasMaxLength(50)
                .HasColumnName("sRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }



    public void registerAccount(string userName, string passWord, string fullName, string dateOfBirth, string phoneNumber, string role)
    {
		string sql = "EXECUTE registerAccount @fullName,@userName,@passWord,@dateOfBirth ,@phoneNumber, @role";
		List<SqlParameter> parameters = new List<SqlParameter>
			{
				 new SqlParameter { ParameterName = "@fullName", Value = fullName },
				 new SqlParameter { ParameterName = "@userName", Value = userName },
				 new SqlParameter { ParameterName = "@passWord", Value = passWord },
				 new SqlParameter { ParameterName = "@dateOfBirth", Value = dateOfBirth },
				 new SqlParameter { ParameterName = "@phoneNumber", Value = phoneNumber },
				 new SqlParameter { ParameterName = "@role", Value = role },
			};
		this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
	}


	public IQueryable<TblUser> loginAccount(string userName, string passWord)
	{

		string sql = "EXECUTE loginAccount @userName,@passWord";
		List<SqlParameter> parameters = new List<SqlParameter>
			{
				  new SqlParameter { ParameterName = "@userName", Value = userName },
				 new SqlParameter { ParameterName = "@passWord", Value = passWord },
			};
		return this.TblUsers.FromSqlRaw(sql, parameters.ToArray());
	}

	public IQueryable<TblUser> checkUserRegister(string userName)
	{

		string sql = "EXECUTE checkUserNameRegister @userName";
		List<SqlParameter> parameters = new List<SqlParameter>
			{
				  new SqlParameter { ParameterName = "@userName", Value = userName },
			};
		return this.TblUsers.FromSqlRaw(sql, parameters.ToArray());
	}

    public IQueryable<TblTheLoai> GetAllTheLoai()
    {

        string sql = "EXECUTE getAllTheLoai" ;
       
        return this.TblTheLoai.FromSqlRaw(sql);
    }
    public void addTheLoai(string tenTheLoai)
    {
        string sql = "EXECUTE addTheLoai @tenTheLoai";
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                  new SqlParameter { ParameterName = "@tenTheLoai", Value = tenTheLoai },
            };
        this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
    }

    public void deleteTheLoai(int id)
    {
        string sql = "EXECUTE deleteTheLoai @id";
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                  new SqlParameter { ParameterName = "@id", Value = id },
            };
        this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
