ALTER procedure [dbo].[USP_NewInchargeProc]                  
(           
@InId as int,                 
@InName as nvarchar(50),          
@InMob as nvarchar(15),          
@desigId as int,          
@DeptId as int,                  
@CreatedBy as nvarchar(100),                  
@type as int,                        
@Active as int,        
@LoginId as nvarchar(50),        
@UserPwd as nvarchar(50),      
@userType as int,
@moduleID AS INT,
@EmailID AS nvarchar(100)
 )                  

as                  

BEGIN                  

IF(@type=1)                  

BEGIN                  

 DECLARE @Incharge_Id INT  

 INSERT INTO Incharge(InName,InMobile,DesigId,DepId,CreatedOn,CreatedBy,Active,LoginId,UserPwd,UserTypeId,ModuleID,EmailID) VALUES(upper(@InName),@InMob,@desigId,@DeptId,GETDATE(),@CreatedBy,@Active,@LoginId,@UserPwd,@userType,@moduleID,@EmailID)                  

 SELECT @Incharge_Id=SCOPE_IDENTITY();  

 INSERT INTO Login(EmailId,Pwd,Active,UserTypeId) VALUES(@LoginId,@UserPwd,@Active,@userType)

 return @Incharge_Id;

END                  

ELSE IF(@type=2)                  

BEGIN                  

 UPDATE Incharge set InName=upper(@InName),InMobile=@InMob, ModIFyOn=GETDATE(), ModIFyBy=@CreatedBy, Active=@Active,LoginId=@LoginId,UserPwd=@UserPwd,EmailID=@EmailID  WHERE InchargeId=@InId  

 --UPDATE Login set EmailId= @LoginId,Pwd=@UserPwd,Active=@Active,UserTypeId=2 WHERE EmailId=@LoginId   

 INSERT INTO Login(EmailId,Pwd,Active,UserTypeId) VALUES(@LoginId,@UserPwd,1,2)      

END                  

ELSE IF (@type=3)                  

BEGIN                  

 DELETE FROM Incharge WHERE InchargeId=@InId            

END                 

ELSE IF (@type=4)                  

BEGIN                  

 UPDATE Incharge set Active=@Active,ModIFyOn=GETDATE(), ModIFyBy=@CreatedBy WHERE  InchargeId=@InId           

END                

END 