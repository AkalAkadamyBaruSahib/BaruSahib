ALTER procedure [dbo].[USP_EstimateMaterialViewForPurchase_V1ByEmployeeID]    --286,2,25             

(                 

@estId as INT,                 

@PsId as INT,        

@EmpID INT        

)                 

as                 

begin                 



SELECT     Estimate.EstId, Material.MatName, Unit.UnitName, EstimateAndMaterialOthersRelations.Qty, CONVERT(nvarchar(20),          

          EstimateAndMaterialOthersRelations.TantiveDate, 107) AS TantiveDate, CONVERT(nvarchar(20), EstimateAndMaterialOthersRelations.DispatchDate, 107)          

            AS DispatchDate, EstimateAndMaterialOthersRelations.Remark, EstimateAndMaterialOthersRelations.DispatchStatus, PurchaseSource.PSName,          

            EstimateAndMaterialOthersRelations.Rate,ISNULL(EstimateAndMaterialOthersRelations.EmployeeAssignDateTime,'') AS EmployeeAssignDateTime,EstimateAndMaterialOthersRelations.remarkByPurchase    

FROM         Estimate INNER JOIN         

               EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN         

                      Material ON EstimateAndMaterialOthersRelations.MatId = Material.MatId INNER JOIN         



                     Unit ON EstimateAndMaterialOthersRelations.UnitId = Unit.UnitId INNER JOIN         











                      PurchaseSource ON EstimateAndMaterialOthersRelations.PSId = PurchaseSource.PSId       































                             































                      WHERE Estimate.EstId=@estId and EstimateAndMaterialOthersRelations.PSId=@PsId       

					        AND EstimateAndMaterialOthersRelations.PurchaseEmpID=@EmpID AND  EstimateAndMaterialOthersRelations.DispatchStatus=0      































                            































end 