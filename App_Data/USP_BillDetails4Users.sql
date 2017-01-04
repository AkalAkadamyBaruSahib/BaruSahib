ALTER procedure [dbo].[USP_BillDetails4Users]  

(  

@logInId as nvarchar(50)  

)      

as      

begin      

SELECT     SubmitBillByUser.SubBillId AS BILL_ID, Zone.ZoneName AS ZONE, Academy.AcaName AS ACADEMY, CONVERT(nvarchar(20), SubmitBillByUser.BillDate, 107)     

                      AS BILL_DATE, SubmitBillByUser.GateEntryNo AS GATENTRY_NO, SubmitBillByUser.StockEntryNo AS STOCKENTERY_NO,     

                      SubmitBillByUser.AgencyName AS AGENCY, Material.MatName AS MATERIAL,     

                      SubmitBillByUserAndMaterialOthersRelation.Qty AS QTY, Unit.UnitName AS UNIT, SubmitBillByUserAndMaterialOthersRelation.Rate AS RATE,     

                      SubmitBillByUserAndMaterialOthersRelation.Amount AS AMOUNT,     

                      CASE SubmitBillByUser.Active WHEN '1' THEN 'ACTIVE' WHEN '1' THEN 'INACTIVE' END AS BILL_STATUS, SubmitBillByUser.BillType AS BILL_TYPE,     

                      SubmitBillByUser.CreatedBy AS CREATED_BY    

FROM         SubmitBillByUser INNER JOIN    

                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN    

                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId INNER JOIN    

                      SubmitBillByUserAndMaterialOthersRelation ON SubmitBillByUser.SubBillId = SubmitBillByUserAndMaterialOthersRelation.SubBillId INNER JOIN    

                      Material ON SubmitBillByUserAndMaterialOthersRelation.MatId = Material.MatId INNER JOIN    

                      Unit ON SubmitBillByUserAndMaterialOthersRelation.UnitId = Unit.UnitId  WHERE SubmitBillByUser.AcaId in (select AcaId from AcademyAssignToEmployee where EmpId =(select InchargeId from Incharge where LoginId=@LoginId))  

end
