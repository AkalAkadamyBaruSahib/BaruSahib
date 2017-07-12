ALTER procedure [dbo].[USP_EstimateMaterialViewForPurchase_ByEmployeeID]                
(                
@estId as int,                
@PsId as int,    
@EmpID INT             
)                
as                
begin                
SELECT     Estimate.EstId, EstimateAndMaterialOthersRelations.PSId, 
Material.MatName,Material.MatTypeID,Material.MatID, Unit.UnitName,Unit.UnitId, 
EstimateAndMaterialOthersRelations.Qty, CONVERT(nvarchar(20),         
EstimateAndMaterialOthersRelations.TantiveDate, 107) AS TantiveDate, 
CONVERT(nvarchar(20), EstimateAndMaterialOthersRelations.DispatchDate, 107)         
AS DispatchDate, EstimateAndMaterialOthersRelations.remarkByPurchase, 
EstimateAndMaterialOthersRelations.DispatchStatus,         
EstimateAndMaterialOthersRelations.Sno,ISNULL(EstimateAndMaterialOthersRelations.PurchaseQty,0.00) AS PurchaseQty,
Material.MatCost AS [Rate],Material.MRP,Material.Discount,Material.Vat,Material.AdditionalDiscount,Material.GST
FROM         Estimate INNER JOIN        
                      EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN        
                      Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN        
                      Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId     
                      where Estimate.EstId=@estId and EstimateAndMaterialOthersRelations.PSId=@PsId     
                      and ISNULL(EstimateAndMaterialOthersRelations.DispatchStatus,0) =0  
                      AND EstimateAndMaterialOthersRelations.PurchaseEmpID=@EmpID     
END 

