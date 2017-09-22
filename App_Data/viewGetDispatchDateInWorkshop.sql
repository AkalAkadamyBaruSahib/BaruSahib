ALTER proc viewGetDispatchDateInWorkshop 
(              

@StartDate datetime,

@EndDate datetime

) 


AS



BEGIN



SELECT distinct convert(varchar(10), W.DispatchOn, 105) AS DISPATCH_DATE FROM WorkshopDispatchMaterial W



INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno

WHERE  W.DispatchOn >= @StartDate and W.DispatchOn <= @EndDate



END