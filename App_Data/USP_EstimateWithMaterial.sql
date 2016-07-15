CREATE PROCEDURE [dbo].[USP_EstimateWithMaterial]  

(

@ModuleID as int                   

) 





as  







begin  







SELECT     Estimate.EstId,CONVERT (NVARCHAR(20),Estimate.ModifyOn,107) AS SanctionDate,Zone.ZoneName,Academy.AcaName, WorkAllot.WorkAllotName, Estimate.SubEstimate, TypeOfWork.TypeWorkName,   







                      MaterialType.MatTypeName,  Material.MatName, PurchaseSource.PSName, EstimateAndMaterialOthersRelations.Qty,   







                      Unit.UnitName, EstimateAndMaterialOthersRelations.Rate, EstimateAndMaterialOthersRelations.Amount, EstimateAndMaterialOthersRelations.Remark   







                        







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



WHERE  Estimate.ModuleID = @ModuleID   order by Estimate.EstId desc



end








