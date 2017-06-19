CREATE PROCEDURE [dbo].[USP_PurchaserPendencyView]         

(        

@PsId as int,

@Incharge as int

)              

AS                

BEGIN   

SELECT distinct E.EstId AS EstimateNumber,

                M.MatName AS MaterialName,

                ER.Qty AS EstimateQuantity,

				U.UnitName,

             	ER.EmployeeAssignDateTime AS EmployeeAssignDateTime

FROM Estimate E     

INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  

INNER JOIN Material M ON M.MatId = ER.MatId

INNER JOIN Unit U ON U.UnitId = ER.UnitId

WHERE ER.PSId = @PsId and ER.DispatchStatus =0 AND ER.PurchaseEmpID= @Incharge AND E.IsApproved=1 

END
