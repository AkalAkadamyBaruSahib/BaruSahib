--(SELECT InName FROM Incharge WHERE InchargeId = ISNULL(EstimateAndMaterialOthersRelations.PurchaseEmpID,0)) AS EmployeeName  







  







CREATE procedure [dbo].[USP_EstimateMaterialViewForAdminUser]              







(              







@estId AS INT ,



@pstId AS INT               







)              







AS              







BEGIN              







SELECT     Estimate.EstId, Material.MatName, Unit.UnitName,ISNULL(INC.InName,'') AS EmployeeName , EstimateAndMaterialOthersRelations.Qty, CONVERT(nvarchar(20),       







                      EstimateAndMaterialOthersRelations.TantiveDate, 107) AS TantiveDate, CONVERT(nvarchar(20), EstimateAndMaterialOthersRelations.DispatchDate, 107)       







                      AS DispatchDate, EstimateAndMaterialOthersRelations.remarkByPurchase, PurchaseSource.PSName, EstimateAndMaterialOthersRelations.Rate,ISNULL(EstimateAndMaterialOthersRelations.EmployeeAssignDateTime,'') AS EmployeeAssignDateTime      














FROM         Estimate INNER JOIN      







                      EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN      







                      Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN      







                      Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId INNER JOIN      







                      PurchaseSource ON EstimateAndMaterialOthersRelations.PSId = PurchaseSource.PSId  







                      LEFT OUTER JOIN Incharge INC ON INC.InchargeId=EstimateAndMaterialOthersRelations.PurchaseEmpID 



					  



					   WHERE Estimate.EstId=@estId and EstimateAndMaterialOthersRelations.PSId IN ((SELECT * FROM dbo.Split( @pstId, ',')))    







                      --WHERE Estimate.EstId=@estId and EstimateAndMaterialOthersRelations.PSId in (2,3)             







END 