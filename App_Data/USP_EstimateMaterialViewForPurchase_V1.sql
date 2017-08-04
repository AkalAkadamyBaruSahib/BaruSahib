







ALTER procedure [dbo].[USP_EstimateMaterialViewForPurchase_V1] --134,2              







(                  







@EstID INT,                  







@PSID INT                  







)                  







AS                  







BEGIN







                  







SELECT     EstimateAndMaterialOthersRelations.Sno, Estimate.EstId, Material.MatName, ISNULL(INC.InName,'') AS EmployeeName,ISNULL(EstimateAndMaterialOthersRelations.EmployeeAssignDateTime,'') AS EmployeeAssignDateTime, Unit.UnitName, EstimateAndMaterialOthersRelations.Qty, CONVERT(nvarchar(20),







  







           







                      EstimateAndMaterialOthersRelations.TantiveDate, 107) AS TantiveDate, CONVERT(nvarchar(20), EstimateAndMaterialOthersRelations.DispatchDate, 107)           







                      AS DispatchDate, EstimateAndMaterialOthersRelations.Remark, EstimateAndMaterialOthersRelations.DispatchStatus, PurchaseSource.PSName,           







                      EstimateAndMaterialOthersRelations.Rate,EstimateAndMaterialOthersRelations.remarkByPurchase              







FROM         Estimate INNER JOIN          







                      EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN          







                      Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN          







                      Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId INNER JOIN          







                      PurchaseSource ON EstimateAndMaterialOthersRelations.PSId = PurchaseSource.PSId         







                      LEFT OUTER JOIN Incharge INC ON INC.InchargeId =  EstimateAndMaterialOthersRelations.PurchaseEmpID        







                      where Estimate.EstId=@EstID and EstimateAndMaterialOthersRelations.PSId=@PSID 







					 and  ISNULL(EstimateAndMaterialOthersRelations.IsApproved,1)=1                







END
