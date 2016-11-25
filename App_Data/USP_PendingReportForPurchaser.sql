CREATE PROCEDURE [dbo].[USP_PendingReportForPurchaser]         



(        



@PsId as int



)              



AS                



BEGIN   



SELECT distinct E.EstId AS EstimateNumber,

                (ISNULL(Incharge.InName,'')) AS EstimateRequestedBy,

                M.MatName AS MaterialName,

                ER.Qty AS EstimateQuantity,

                Z.ZoneName AS Zone,

                A.AcaName AS Academy,

                (ISNULL(INC.InName,'')) AS PurchaserName,

         		E.ModifyOn AS SanctionDate

FROM Estimate E     



INNER JOIN Academy A ON A.AcaId=E.AcaId      



INNER JOIN Zone Z ON Z.ZoneId=E.ZoneId      



INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  



INNER JOIN Incharge INC ON INC.InchargeID=ER.PurchaseEmpID 



INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId



INNER JOIN Material M ON M.MatId = ER.MatId



INNER JOIN Incharge Incharge ON Incharge.InchargeID=E.CreatedBy 



WHERE ER.PSId = @PsId and ER.DispatchStatus =0



END
