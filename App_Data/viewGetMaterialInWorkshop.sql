ALTER proc viewGetMaterialInWorkshop 
(              
@StartDate datetime,
@EndDate datetime
) 
AS
BEGIN
SELECT M.MatName,U.UnitName,M.MatId FROM WorkshopDispatchMaterial W
INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno
INNER JOIN Material M ON M.MatId = EMR.MatId
INNER JOIN Unit U ON M.UnitId = U.UnitId 
WHERE  W.DispatchOn >= @StartDate and W.DispatchOn <= @EndDate
group by M.MatName,U.UnitName,M.MatId
END