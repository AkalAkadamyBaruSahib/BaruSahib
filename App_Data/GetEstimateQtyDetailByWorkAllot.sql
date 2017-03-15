CREATE proc GetEstimateQtyDetailByWorkAllot 



@WaId INT,



@AcaId INT,



@PSID INT



AS



BEGIN



SELECT ER.Qty, e.EstId,M.MatId 



FROM [dbo].[EstimateAndMaterialOthersRelations] ER 



INNER JOIN Estimate E on E.EstId=ER.EstId 



INNER JOIN Material M on M.MatId = ER.MatId 



WHERE ER.MatId in(SELECT M.MatId from [dbo].[EstimateAndMaterialOthersRelations] ER INNER JOIN Estimate E on E.EstId=ER.EstId  



WHERE E.WAId=@WaId and E.AcaId =@AcaId and ER.PSId=@PSID) 



and E.WAId=@WaId



and E.AcaId =@AcaId



and ER.PSId=@PSID



END