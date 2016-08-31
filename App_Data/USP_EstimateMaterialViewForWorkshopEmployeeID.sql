ALTER procedure [dbo].[USP_EstimateMaterialViewForWorkshopEmployeeID]       

(                

@estId as int,                

@PsId as int,    

@EmpID INT             

)                

as                

begin                

SELECT     Estimate.EstId, EstimateAndMaterialOthersRelations.PSId,Material.MatName,Material.MatTypeID,Material.MatID,Unit.UnitName,
           Unit.UnitId,Material.MatCost,EstimateAndMaterialOthersRelations.Qty,EstimateAndMaterialOthersRelations.DispatchStatus,       
           EstimateAndMaterialOthersRelations.Sno,ISNULL(WorkshopStoreMaterial.InStoreQty,'0') AS InStoreQty,
           (SELECT  ISNULL(SUM(WorkshopDispatchMaterial.DispatchQty),'0') FROM WorkshopDispatchMaterial WHERE WorkshopDispatchMaterial.EMRID = EstimateAndMaterialOthersRelations.Sno) AS  DispatchQty
FROM         Estimate INNER JOIN 
             EstimateAndMaterialOthersRelations ON 
			 Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN           
			 Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId LEFT OUTER JOIN
             WorkshopStoreMaterial ON WorkshopStoreMaterial.MatID = Material.MatId  INNER JOIN
			 Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId  
WHERE        Estimate.EstId=@estId and EstimateAndMaterialOthersRelations.PSId=@PsId
             AND ISNULL(EstimateAndMaterialOthersRelations.DispatchStatus,0) =0  
             AND EstimateAndMaterialOthersRelations.PurchaseEmpID=@EmpID     
END 
