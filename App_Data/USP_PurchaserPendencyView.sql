ALTER PROCEDURE [dbo].[USP_PurchaserPendencyView]         

(        

@PsId as int,

@Incharge as int

)              

AS               

BEGIN   

SELECT distinct E.EstId AS EstimateNumber,

                M.MatName AS MaterialName,

                ER.Qty AS EstimateQuantity,

				(ER.Qty - ER.PurchaseQty)  AS PendingQuantity,

				U.UnitName,

             	ER.EmployeeAssignDateTime AS EmployeeAssignDateTime,

				(ISNULL(INC.InName,'')) AS PurchaserName

FROM Estimate E     

INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  

INNER JOIN Material M ON M.MatId = ER.MatId

INNER JOIN Unit U ON U.UnitId = ER.UnitId

INNER JOIN Incharge INC ON INC.InchargeID=ER.PurchaseEmpID 

WHERE ER.PSId = @PsId and ER.DispatchStatus =0 AND ER.PurchaseEmpID= @Incharge AND E.IsApproved=1 

END
