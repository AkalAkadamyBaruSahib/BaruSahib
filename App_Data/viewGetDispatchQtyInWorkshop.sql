CREATE proc viewGetDispatchQtyInWorkshop 
(              
@BillID int
)  
AS
BEGIN
SELECT W.DispatchQty,M.MatId,
datename(DAY,W.DispatchOn)+' '+datename(M,W.DispatchOn)+' '+cast(datepart(YYYY,W.DispatchOn) as varchar) AS DISPATCH_DATE,
DispatchQty* DispatchRate AS Amount  FROM WorkshopDispatchMaterial W
INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno
INNER JOIN Material M ON M.MatId = EMR.MatId
WHERE  W.BillNo =@BillID
END