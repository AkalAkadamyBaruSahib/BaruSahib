ALTER procedure [dbo].[USP_NewSubmitBillByUser]                              

(                              

@SubBillId as int,                              

@BillTYpeIdChargeableTo as int,                              

@BillDate as datetime,                              

@GateEntyNo as nvarchar(50),                      

@StockEnteryNo as nvarchar(50),                                      

@AgencyName as nvarchar(50), 

@Remark as nvarchar(50),                  

@type as int,                                        

@Active as int,                  

@CreatedModifyBy as nvarchar(50),                  

@EstId as int,                

@AcaId as int,                

@ZoneId as int,                

@NatureOfBill as nvarchar(50),              

@BillTypeText as nvarchar(50),        

@NameOfWork as int,

@VendorBillNumber  as int,

@VendorBillPath  as varchar(100)    

)                              

as                              

begin                              

if(@type=1)                              

begin                              

declare @BillId as int      

insert into SubmitBillByUser(ChargetoBillTyId,BillDate,GateEntryNo,StockEntryNo,AgencyName,Remark,Active,CreatedOn,CreatedBy,EstId,AcaId,ZoneId,NatureOfBill,BillType,WAId,VendorBillPath,VendorBillNumber) 

values(@BillTYpeIdChargeableTo,@BillDate,upper(@GateEntyNo),upper(@StockEnteryNo),upper(@AgencyName),upper(@Remark),@Active,GETDATE(),@CreatedModifyBy,@EstId,@AcaId,@ZoneId,@NatureOfBill,@BillTypeText,@NameOfWork,@VendorBillPath,@VendorBillNumber)                              

set @BillId=(select MAX(SubBillId) from SubmitBillByUser WHERE CreatedBy=@CreatedModifyBy)      

insert into BillProceedLog(BillId) values (@BillId)      

end                              

else if(@type=2)                              







begin                              







update SubmitBillByUser set BillType=@BillTypeText,BillDate=@BillDate,GateEntryNo=upper(@GateEntyNo),StockEntryNo=upper(@StockEnteryNo),AgencyName=upper(@AgencyName),Remark=upper(@Remark),Active=@Active,VendorBillPath=@VendorBillPath,VendorBillNumber=@VendorBillNumber,ModifyOn











=GETDATE(),  







FirstVarify = NULL,  







FirstVarifyOn = NULL,  







FirstVarifyRemark = NULL,  







FirstVarifyStatus = NULL,  







SecondVarifyStatus = NULL,  







SeccondVarify = NULL,  







SeccondVarifyOn = NULL,  







SecondVarifyRemark = NULL,  







ModifyBy=@CreatedModifyBy,ChargetoBillTyId=@BillTYpeIdChargeableTo,WAId=@NameOfWork where SubBillId=@SubBillId                      







end                              







else if (@type=3)                              







begin                              







delete from SubmitBillByUser where SubBillId=@SubBillId                                







end                       







else if (@type=4)                      







begin                      







update SubmitBillByUser set Active=@Active,ModifyOn=GETDATE(),ModifyBy=@CreatedModifyBy where SubBillId=@SubBillId                       







end                             







end   
