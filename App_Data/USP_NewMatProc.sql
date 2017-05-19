ALTER procedure [dbo].[USP_NewMatProc]
(              
@MatName as nvarchar(250),              
@MatCost as decimal(16,2),              
@MatTypeId as int,              
@CreatedBy as nvarchar(100),              
@type as int,              
@MatId as int,              
@Active as int ,      
@UnitId as int,            
@ImageUrl VARCHAR(500),
@LocalRate as decimal(16,2),
@AkalWorkshopRate as decimal(16,2),
@Discount as decimal(18,2),
@Vat as decimal(18,2),
@MRP as decimal(18,2)
)             
as             
begin              
if(@type=1)              
begin              
insert into Material(MatTypeId,MatName,MatCost,CreatedOn,CreatedBy,Active,UnitId,ImageUrl,LocalRate,AkalWorkshopRate,Discount,Vat,MRP) values(@MatTypeId,upper(@MatName),@MatCost,GETDATE(),@CreatedBy,@Active,@UnitId,@ImageUrl,@LocalRate,@AkalWorkshopRate,@Discount,@Vat,@MRP)              
end              
else if(@type=2)              
begin              
if(@ImageUrl!='')
BEGIN
update Material set MatName=upper(@MatName),MatCost=@MatCost, ModifyOn=GETDATE(),MatTypeId=@MatTypeId, ModifyBy=@CreatedBy,ImageUrl=@ImageUrl, Active=@Active,UnitId=@UnitId,AkalWorkshopRate=@AkalWorkshopRate where MatId=@MatId              
END
ELSE
BEGIN
update Material set MatName=upper(@MatName),MatCost=@MatCost, ModifyOn=GETDATE(),MatTypeId=@MatTypeId, ModifyBy=@CreatedBy, Active=@Active,UnitId=@UnitId,AkalWorkshopRate=@AkalWorkshopRate where MatId=@MatId              
END
end              
else if (@type=3)              
begin              
delete from Material where MatId=@MatId              
end              
else if (@type=4)              
begin              
update Material set Active=@Active, ModifyOn=GETDATE(), ModifyBy=@CreatedBy where MatId=@MatId            
end       
else if (@type=5)              
begin              
update Material set Active=@Active, ModifyOn=GETDATE(), ModifyBy=@CreatedBy,MatTypeId=@MatTypeId,ChangeMatTypeStatus=1,ChangeMatTypeOn=GETDATE() where MatId=@MatId            
end           
end
