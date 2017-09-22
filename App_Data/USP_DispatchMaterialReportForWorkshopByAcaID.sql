ALTER procedure [dbo].[USP_DispatchMaterialReportForWorkshopByAcaID]            

(              

@StartDate datetime,

@EndDate datetime,

@AcaID as int

)              

AS                

BEGIN 

SELECT datename(DAY,W.DispatchOn)+' '+datename(M,W.DispatchOn)+' '+cast(datepart(YYYY,W.DispatchOn) as varchar) AS BILL_DATE,

M.MatName AS PARTICULARS,W.DispatchQty AS QTY,U.UnitName AS UNIT_NAME,

W.BillNo AS BILL_NO

FROM WorkshopDispatchMaterial W

INNER JOIN Estimate E On E.EstId = W.EstID

INNER JOIn Academy A ON A.AcaId = E.AcaId

INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno

INNER JOIN Unit U ON U.UnitId = EMR.UnitId

INNER JOIN Material M ON M.MatId = EMR.MatId

WHERE E.AcaId=@AcaID AND W.DispatchOn >= @StartDate and W.DispatchOn <= @EndDate

END
