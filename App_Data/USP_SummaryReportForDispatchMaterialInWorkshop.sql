CREATE procedure [dbo].[USP_SummaryReportForDispatchMaterialInWorkshop]            

(              

@StartDate datetime,

@EndDate datetime

)              

AS                

BEGIN 

SELECT A.AcaName AS AKAL_ACADEMY,W.BillNo AS BILL_NO

FROM WorkshopDispatchMaterial W

INNER JOIN Estimate E On E.EstId = W.EstID

INNER JOIn Academy A ON A.AcaId = E.AcaId

INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno

INNER JOIN Material M ON M.MatId = EMR.MatId 

WHERE  W.DispatchOn >= @StartDate and W.DispatchOn <= @EndDate GROUP BY A.AcaName,W.BillNo 

ORDER BY BillNo ASC



END
