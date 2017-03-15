CREATE proc GetSubmitBillQtyDetailByWorkAllot 



@WaId INT,



@AcaId INT,



@PSID INT



AS



BEGIN



SELECT SBR.Qty, SB.SubBillId,SBR.MatId 



FROM SubmitBillByUserAndMaterialOthersRelation SBR 



INNER JOIN SubmitBillByUser SB on SB.SubBillId=SBR.SubBillId



 INNER JOIN Material M on M.MatId = SBR.MatId 



WHERE SB.WAId=@WaId and SB.AcaId = @AcaId 



and SBR.MatId in(SELECT M.MatId from EstimateAndMaterialOthersRelations ER INNER JOIN Estimate E on E.EstId=ER.EstId 



WHERE E.WAId= @WaId and E.AcaId = @AcaId and ER.PSId = @PSID)



END
