ALTER procedure [dbo].[USP_AdminBillViewByBillId_V2]               







(                 







@BillId as int                  







)                 







as                 







begin        







--Bill Varification Details                  







SELECT     SubmitBillByUser.SubBillId,SubmitBillByUser.WAId, CONVERT(nvarchar(20), SubmitBillByUser.BillDate, 107) AS BillDate, SubmitBillByUser.TotalAmount, SubmitBillByUser.AgencyName,         







                      Academy.AcaName, Zone.ZoneName, SubmitBillByUser.FirstVarify, SubmitBillByUser.FirstVarifyRemark, CONVERT(nvarchar(20), SubmitBillByUser.FirstVarifyOn,        







                      107) AS FirstVarifyOn, BillType.BillTypeName, SubmitBillByUser.BillType, SubmitBillByUser.GateEntryNo, SubmitBillByUser.EstId,        







                      SubmitBillByUser.FirstVarifyStatus, SubmitBillByUser.SecondVarifyStatus, SubmitBillByUser.PaymentStatus, SubmitBillByUser.SeccondVarify,        







                      CONVERT(nvarchar(20), SubmitBillByUser.SeccondVarifyOn, 107) AS SeccondVarifyOn, SubmitBillByUser.SecondVarifyRemark, SubmitBillByUser.ReciptNoByEmp,        







                      CONVERT(nvarchar(20), SubmitBillByUser.DateOfReceving, 107) AS DateOfReceving, SubmitBillByUser.RecevingRemark, SubmitBillByUser.RecevingStatus,        







                      SubmitBillByUser.RecevingBy, SubmitBillByUser.ThirdVarifyBy, SubmitBillByUser.ThirdVarifyRemark, CONVERT(nvarchar(20), SubmitBillByUser.ThirdVarifyOn, 107)        







                      AS ThirdVarifyOn,SubmitBillByUser.VendorBillPath As AgencyBill,SubmitBillByUser.VendorBillNumber As AgencyBillNumber       







FROM         SubmitBillByUser INNER JOIN       







                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN       







                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId INNER JOIN       







                      BillType ON SubmitBillByUser.ChargetoBillTyId = BillType.BillTypeId where SubmitBillByUser.SubBillId=@BillId                 







--Payment Details       







SELECT     PaymentMode.PayModeName, PaymentDetailByAccount.Remark, PaymentDetailByAccount.PayDetails       







FROM         PaymentDetailByAccount INNER JOIN       







                      PaymentMode ON PaymentDetailByAccount.PayModeId = PaymentMode.PayModeId where PaymentDetailByAccount.BillId=@BillId       







        







--Bill Material Details                                







SELECT     SubmitBillByUserAndMaterialOthersRelation.Sno   ,SubmitBillByUserAndMaterialOthersRelation.StockEntryNo, SubmitBillByUserAndMaterialOthersRelation.Rate, SubmitBillByUserAndMaterialOthersRelation.Amount,      







                      Material.MatName, Unit.UnitName, SubmitBillByUserAndMaterialOthersRelation.SubBillId, SubmitBillByUserAndMaterialOthersRelation.Remark,      







                      SubmitBillByUserAndMaterialOthersRelation.Qty, SubmitBillByUserAndMaterialOthersRelation.MatId, SubmitBillByUserAndMaterialOthersRelation.Vat    







FROM         SubmitBillByUserAndMaterialOthersRelation INNER JOIN     







                      Material ON SubmitBillByUserAndMaterialOthersRelation.MatId = Material.MatId INNER JOIN     







                      Unit ON SubmitBillByUserAndMaterialOthersRelation.UnitId = Unit.UnitId where SubmitBillByUserAndMaterialOthersRelation.SubBillId=@BillId                 







end
