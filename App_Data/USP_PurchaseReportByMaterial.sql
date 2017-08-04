ALTER PROCEDURE [dbo].[USP_PurchaseReportByMaterial]         
(        
@StartDate datetime,
@EndDate datetime,
@PsId as int,
@MatName as nvarchar(250)
)              
AS                
BEGIN      
SELECT DISTINCT EMR.Qty,
EMR.Rate,
EMR.Qty * EMR.Rate AS Amount 
FROM EstimateAndMaterialOthersRelations EMR 
INNER JOIN Material M ON M.MatId=EMR.MatId
WHERE EMR.DispatchStatus = 1 AND EMR.PSId=@PsId AND M.MatName= @MatName
AND EMR.DispatchDate >= @StartDate and EMR.DispatchDate <= @EndDate
END