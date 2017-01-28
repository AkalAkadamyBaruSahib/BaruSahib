ALTER procedure [dbo].[USP_BillDetails]      

as      

begin      

SELECT     SubmitBillByUser.SubBillId AS BILL_ID, Zone.ZoneName AS ZONE, Academy.AcaName AS ACADEMY,

           SubmitBillByUser.EstId aS EST_NO, Estimate.SubEstimate AS ESTIMATE, WorkAllot.WorkAllotName AS WORK_NAME,

           CONVERT(nvarchar(20), SubmitBillByUser.BillDate, 107) AS BILL_DATE, SubmitBillByUser.GateEntryNo AS GATENTRY_NO,

           SubmitBillByUser.StockEntryNo AS STOCKENTERY_NO, SubmitBillByUser.AgencyName  AS AGENCY, Material.MatName AS MATERIAL,   

           SubmitBillByUserAndMaterialOthersRelation.Qty AS QTY, Unit.UnitName AS UNIT, 

           SubmitBillByUserAndMaterialOthersRelation.Rate AS RATE, SubmitBillByUserAndMaterialOthersRelation.Amount AS AMOUNT,   

           CASE SubmitBillByUser.Active WHEN '1' THEN 'ACTIVE' WHEN '1' THEN 'INACTIVE' END AS BILL_STATUS,

		   CASE SubmitBillByUser.BillType WHEN '1' THEN 'SANCTIONED' WHEN '2' THEN 'NON SANCTIONED' END AS BILL_TYPE,

           Incharge.InName AS CREATED_BY  

FROM       SubmitBillByUser INNER JOIN  

           Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN  

           Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId INNER JOIN  

           SubmitBillByUserAndMaterialOthersRelation ON SubmitBillByUser.SubBillId = SubmitBillByUserAndMaterialOthersRelation.SubBillId INNER JOIN  

           Material ON SubmitBillByUserAndMaterialOthersRelation.MatId = Material.MatId INNER JOIN  

           Unit ON SubmitBillByUserAndMaterialOthersRelation.UnitId = Unit.UnitId  LEFT OUTER JOIN   

           WorkAllot ON SubmitBillByUser.WAId = WorkAllot.WAId INNER JOIN 

     	   Incharge ON SubmitBillByUser.CreatedBy = Incharge.InchargeId LEFT OUTER JOIN 

           Estimate ON SubmitBillByUser.EstId = Estimate.EstId  order by SubmitBillByUser.SubBillId desc

end
