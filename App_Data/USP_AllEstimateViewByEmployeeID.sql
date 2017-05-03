ALTER PROCEDURE [dbo].[USP_AllEstimateViewByEmployeeID]         
(
@empID INT
)
as            
begin           
SELECT distinct  Estimate.EstId,Estimate.EstmateCost,CONVERT(NVARCHAR(20), Estimate.SanctionDate, 107) AS dt,Estimate.SubEstimate,
                 Zone.ZoneName,Academy.AcaName,  Academy.AcaID,WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.CreatedOn, Estimate.ModifyOn  
FROM             Estimate 
                 INNER JOIN   EstimateAndMaterialOthersRelations ER ON ER.EstId = Estimate.EstId
                 INNER JOIN  Zone ON Estimate.ZoneId = Zone.ZoneId
                 INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId 
                 INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId 
			 WHERE Estimate.Active=1  AND ER.PurchaseEmpID=@empID
	         order by      Estimate.EstId desc     
END
