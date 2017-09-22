ALTER procedure [dbo].[USP_DispatchMaterialReportForWorkshopByMatID]            
(              
@StartDate datetime,
@EndDate datetime,
@MatID as int
)              
AS                
BEGIN 
SELECT datename(DAY,W.DispatchOn)+' '+datename(M,W.DispatchOn)+' '+cast(datepart(YYYY,W.DispatchOn) as varchar) AS DISPATCH_DATE,
W.BillNo AS BILL_NO,
A.AcaName AS AKAL_ACADEMY,W.DispatchQty AS ISSUED
FROM WorkshopDispatchMaterial W
INNER JOIN Estimate E On E.EstId = W.EstID
INNER JOIn Academy A ON A.AcaId = E.AcaId
INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno
WHERE EMR.MatId=@MatID AND W.DispatchOn >= @StartDate and W.DispatchOn <= @EndDate
END
