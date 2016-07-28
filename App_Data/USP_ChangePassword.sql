ALTER procedure [dbo].[USP_ChangePassword]
(
	@LoginId as nvarchar(50),
	@pwd as nvarchar(50)
)
AS
BEGIN
IF(@LoginId='ADMIN')
BEGIN
	UPDATE LOGIN SET Pwd=@pwd ,ChangePwdOn=GETDATE()  where EmailId=@LoginId
	UPDATE INCHARGE SET UserPwd=@pwd, ChangePwdOn=GETDATE() where LoginId=@LoginId
end

else

begin

update Login set Pwd=@pwd ,ChangePwdOn=GETDATE()  where EmailId=@LoginId

update Incharge set UserPwd=@pwd, ChangePwdOn=GETDATE() where LoginId=@LoginId

end

end


