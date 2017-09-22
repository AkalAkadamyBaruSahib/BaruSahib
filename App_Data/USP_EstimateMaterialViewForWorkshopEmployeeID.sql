ALTER procedure [dbo].[USP_EstimateMaterialViewForWorkshopEmployeeID] --3747,3,108
(                
@estId as int,                
@PsId as int,    
@EmpID INT
)                
as                
begin                
SELECT    distinct Estimate.EstId, EstimateAndMaterialOthersRelations.PSId,Material.MatName,Material.MatTypeID,
          Material.MatID,Unit.UnitName,Unit.UnitId,Material.AkalWorkshopRate,
		  EstimateAndMaterialOthersRelations.Qty,EstimateAndMaterialOthersRelations.DispatchStatus, 
		  ISNULL(EstimateAndMaterialOthersRelations.PurchaseQty,0.00) AS DispatchQty, EstimateAndMaterialOthersRelations.Sno,     
         -- ISNULL(WSM.InStoreQty,'0') AS InStoreQty,
		 -- (SELECT  ISNULL(SUM(WorkshopDispatchMaterial.DispatchQty),'0')
		 -- FROM WorkshopDispatchMaterial 
	     -- WHERE WorkshopDispatchMaterial.EMRID = EstimateAndMaterialOthersRelations.Sno) AS  DispatchQty,
		  Material.MRP,Material.Discount,Material.Vat, Material.AdditionalDiscount,Material.GST
FROM      Estimate 
		  INNER JOIN EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId 
		  INNER JOIN Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId 
	      INNER JOIN AcademyAssignToEmployee AAE ON AAE.EmpId=@EmpID  
		  INNER JOIN WorkshopStoreMaterial WSM ON WSM.AcaID = AAE.AcaId
          INNER JOIN Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId  
WHERE     Estimate.EstId=@estId 
    	  AND EstimateAndMaterialOthersRelations.PSId=@PsId
          AND ISNULL(EstimateAndMaterialOthersRelations.DispatchStatus,0) =0  
          AND EstimateAndMaterialOthersRelations.PurchaseEmpID=@EmpID 
END 
