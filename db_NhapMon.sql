-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE <Procedure_Name, sysname, ProcedureName> 
	-- Add the parameters for the stored procedure here
	<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
END
GO


create PROCEDURE registerAccount
@fullName nvarchar(50),@userName nvarchar(50),@passWord nvarchar(50),@dateOfBirth date,
@phoneNumber nvarchar(50), @role nvarchar(50)
AS
BEGIN
	insert into tblUser(sUserName,sFullName,sPassword,sPhoneNumber,dDateOfBirth,sRole)
	values (@userName,@fullName,@passWord,@phoneNumber,@dateOfBirth,@role)
END
GO

CREATE PROCEDURE checkUserNameRegister
@userName nvarchar(50)
AS
BEGIN
	select *
	From tblUser
	where tblUser.sUserName= @userName
END
GO


create PROCEDURE loginAccount
@userName nvarchar(50),@passWord nvarchar(50)
AS
BEGIN
	select *
	From tblUser
	where tblUser.sUserName= @userName and tblUser.sPassword=@passWord
END
GO


create proc getAllTheLoai
as
begin
select * from tblTheLoai
end
go

exec getAllTheLoai

create proc addTheLoai
@tenTheLoai nvarchar(max)
as
begin
insert into tblTheLoai (sTenTheLoai)
values (@tenTheLoai)
end
go

exec addTheLoai N'Hoạt hình'


create proc deleteTheLoai
@id bigint
as
begin
delete tblTheLoai
where tblTheLoai.PK_iTheLoaiID=@id
end
go

--phòng

create proc getPhong
as
begin
select *
from tblPhong
end
go

create proc getPhongByID
@id bigint
as
begin
select *
from tblPhong
where tblPhong.PK_iPhongID=@id
end
go

create proc addPhong
@tenPhong nvarchar(max),@soLuongGhe bigint
as
begin
insert into tblPhong(sTenPhong, iSoLuongGhe)
values (@tenPhong, @soLuongGhe)
end
go

alter proc editPhong
@tenPhong nvarchar(max),@soLuongGhe bigint, @id bigint
as
begin
update tblPhong
set iSoLuongGhe=@soLuongGhe,sTenPhong=@tenPhong
where PK_iPhongID=@id
end
go

exec editPhong N'Phong 1', 60,1

create proc deletePhong
@id bigint
as
begin
delete tblPhong
where tblPhong.PK_iPhongID=@id
end
go