ALTER PROCEDURE [dbo].[USP_EstimateDetailByPurchaseAdmin]   

(  

@EstId INT,

@UserID INT  

)  

AS  

BEGIN  

SELECT     Estimate.EstId, CONVERT(NVARCHAR(20), Estimate.SanctionDate, 107) AS SanctionDate, Zone.ZoneName, Academy.AcaName, WorkAllot.WorkAllotName,   

           Estimate.SubEstimate, TypeOfWork.TypeWorkName, Estimate.EstmateCost,Incharge.InName,Incharge.InMobile  

FROM         Estimate INNER JOIN  

             TypeOfWork ON Estimate.TypeWorkId = TypeOfWork.TypeWorkId INNER JOIN  

             WorkAllot ON Estimate.WAId = WorkAllot.WAId INNER JOIN  

             Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN  

              Zone ON Estimate.ZoneId = Zone.ZoneId  INNER JOIN

	     Incharge ON Incharge.InchargeId  = Estimate.ModifyBy















WHERE     (Estimate.EstId = @EstId)  















SELECT     Estimate.EstId,  Material.MatName, PurchaseSource.PSName,EstimateAndMaterialOthersRelations.Qty AS EstQty,   















                      EstimateAndMaterialOthersRelations.PurchaseQty AS PurchaseQty, Unit.UnitName, EstimateAndMaterialOthersRelations.Rate, EstimateAndMaterialOthersRelations.Amount,   















                      EstimateAndMaterialOthersRelations.Remark,MaterialType.MatTypeName















FROM         Estimate INNER JOIN  















                      EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN  















                      MaterialType ON EstimateAndMaterialOthersRelations.MatTypeId = MaterialType.MatTypeId INNER JOIN  















                      Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN  















                      PurchaseSource ON EstimateAndMaterialOthersRelations.PSId = PurchaseSource.PSId INNER JOIN  















                      Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId  















WHERE     Estimate.EstId = @EstId AND EstimateAndMaterialOthersRelations.PSId=2















SELECT SUM(Amount) as Ttl from EstimateAndMaterialOthersRelations where EstId=@EstId AND PSId=2















END
































































