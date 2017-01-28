ALTER procedure [dbo].[USP_AdminBillView_V2]            

as            

begin            

SELECT DISTINCT   

                      SubmitBillByUser.SubBillId, CONVERT(nvarchar(20), SubmitBillByUser.BillDate, 107) AS BillDate, SubmitBillByUser.TotalAmount, SubmitBillByUser.AgencyName,   

                      Academy.AcaName, Zone.ZoneName, SubmitBillByUser.FirstVarify, SubmitBillByUser.FirstVarifyRemark, SubmitBillByUser.FirstVarifyOn, BillType.BillTypeName,   

                      SubmitBillByUser.BillType, SubmitBillByUser.FirstVarifyStatus, Material.Active AS MatStatus, Unit.Active AS UnitStatus, BillProceedLog.AdminProStatus,   

                      BillProceedLog.AuditProStatus, BillProceedLog.AccProStatus, BillProceedLog.UserProStatus, BillProceedLog.PurProStatus  

FROM         SubmitBillByUser INNER JOIN  

                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN  

                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId LEFT OUTER JOIN  

                      BillType ON SubmitBillByUser.ChargetoBillTyId = BillType.BillTypeId INNER JOIN  

                      SubmitBillByUserAndMaterialOthersRelation ON SubmitBillByUser.SubBillId = SubmitBillByUserAndMaterialOthersRelation.SubBillId INNER JOIN  

                      Material ON SubmitBillByUserAndMaterialOthersRelation.MatId = Material.MatId INNER JOIN  

                      Unit ON SubmitBillByUserAndMaterialOthersRelation.UnitId = Unit.UnitId LEFT OUTER JOIN  

                      BillProceedLog ON SubmitBillByUser.SubBillId = BillProceedLog.BillId where SubmitBillByUser.FirstVarifyStatus is null or   SubmitBillByUser.FirstVarifyStatus=0  or BillProceedLog.AdminProStatus=1 or BillProceedLog.AuditProStatus=1 or


 BillProceedLog.AccProStatus=1 or BillProceedLog.UserProStatus=1 or BillProceedLog.PurProStatus=1    order by SubmitBillByUser.SubBillId desc

end
