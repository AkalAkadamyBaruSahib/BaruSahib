ALTER procedure [dbo].[USP_NewSubmitBillByUserAndMaterialOthersRelations]                   



(             



@sn as int,                 



@SubBillId as int,          



@MatTypeId as int,                  



@MatId as int,         



@itemName as nvarchar(50),                     



@Qty as decimal(18,2),                   



@UnitId as int,           



@Rate as decimal(18,2),           



@Amount as decimal(18,2),                   



@CreatedBy as int,                   



@type as int,           



@Active as int,       



@Remark as nvarchar(50),       



@StockEnterNo as nvarchar(50),     



@WorkAllotID as int,



@Vat as decimal(18,2)      



)                   



as                   



begin              



--  declare @EId as int         



             



   --select @EId         



if(@type=1)                    



begin                   



      Select @SubBillId=ISNULL(MAX(SubBillId),1) from SubmitBillByUser where CreatedBy=@CreatedBy  



      insert into SubmitBillByUserAndMaterialOthersRelation(SubBillId,MatTypeId,MatId,ItemName,Qty,UnitId,Rate,Amount,Active,CreatedOn,CreatedBy,Remark,StockEntryNo,Vat) values(@SubBillId,@MatTypeId,@MatId,@itemName,@Qty,@UnitId,@Rate,@Amount,@Active,GETDATE(),@CreatedBy,upper(@Remark),upper(@StockEnterNo),@Vat)                   



      insert into BillMaterialQtyLog(BillId,MatId,DrQty,CreateOn,CreatedBy,Active,WorkAllotID) values (@SubBillId,@MatId,@Qty,GETDATE(),@CreatedBy,1,@WorkAllotID)     



end                   



else if(@type=2)                   



begin         



                    



update SubmitBillByUserAndMaterialOthersRelation set SubBillId=@SubBillId,MatTypeId=@MatTypeId,MatId=@MatId,ItemName=@itemName,Qty=@Qty,UnitId=@UnitId,Rate=@Rate,Amount=@Amount,Active=@Active,ModifyOn=GETDATE(),ModifyBy=@CreatedBy,Vat=@Vat where Sno=@sn  
         





end                   



else if (@type=3)                    



begin                   



delete from SubmitBillByUserAndMaterialOthersRelation where Sno=@sn              



end            



else if (@type=4)           



begin           



update SubmitBillByUserAndMaterialOthersRelation set Active=@Active,ModifyBy=@CreatedBy,ModifyOn=GETDATE() where Sno=@sn              



end                  



end
