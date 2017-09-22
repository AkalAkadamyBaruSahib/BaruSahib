CREATE procedure [dbo].[USP_GetMaterialDispatchQtyInWorkshop]            
(              
@MatID int,
@BillID int
)              
AS                
BEGIN 
SELECT M.MatName,W.DispatchQty
FROM WorkshopDispatchMaterial W
INNER JOIN EstimateAndMaterialOthersRelations EMR ON W.EMRID = EMR.Sno
INNER JOIN Material M ON M.MatId = EMR.MatId 
WHERE  W.MatID = @MatID and W.BillNo = @BillID  
END
