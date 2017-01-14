ALTER PROCEDURE [dbo].[USP_EstimateWithMaterialByPurchaser]  
(
@EmpID as int                   
) 
AS  
BEGIN  
SELECT     Estimate.EstId,
           CONVERT (NVARCHAR(20),Estimate.ModifyOn,107) AS SanctionDate,
		   Zone.ZoneName,
		   Academy.AcaName,
		   WorkAllot.WorkAllotName,
		   TypeOfWork.TypeWorkName,
		   PurchaseSource.PSName,
		   Estimate.SubEstimate,
	       MaterialType.MatTypeName,
		   Material.MatName,
		   Unit.UnitName,
		   EstimateAndMaterialOthersRelations.Qty,   
     	   EstimateAndMaterialOthersRelations.Rate,
		   EstimateAndMaterialOthersRelations.Amount,
		   EstimateAndMaterialOthersRelations.Remark   
FROM         Estimate INNER JOIN  
             EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN  
             TypeOfWork ON Estimate.TypeWorkId = TypeOfWork.TypeWorkId INNER JOIN  
             WorkAllot ON Estimate.WAId = WorkAllot.WAId INNER JOIN  
             MaterialType ON EstimateAndMaterialOthersRelations.MatTypeId = MaterialType.MatTypeId INNER JOIN  
             Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN  
             Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  
             Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN  
             PurchaseSource ON EstimateAndMaterialOthersRelations.PSId = PurchaseSource.PSId INNER JOIN  
             Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId  
WHERE  EstimateAndMaterialOthersRelations.PurchaseEmpID = @EmpID   ORDER BY Estimate.EstId DESC
END
















