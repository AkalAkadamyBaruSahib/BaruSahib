CREATE VIEW view_BillSubmitedDetails



AS



SELECT ES.EstID AS EstimateNumber,



A.AcaName AS AcademyName,



WA.WorkAllotName,



B.SubBillId As BillNumber,



B.AgencyName,



M.MatName,



EM.Qty AS EstQuantity,



EM.Rate AS EstRate,



EM.Amount as EstAmount,



BMR.Qty as PurchasedQTY,



BMR.Rate as PurchasedRate,



ES.IsApproved,EM.PSId,ES.WAId,A.AcaId









 FROM Estimate  ES      







 INNER JOIN EstimateAndMaterialOthersRelations EM ON ES.EstId=EM.EstId      







 INNER JOIN WorkAllot WA ON WA.WAId=ES.WAId      







 INNER JOIN Material M ON EM.MatId = M.MatId 







 INNER JOIN Academy A ON A.AcaId = ES.AcaId  







 LEFT OUTER JOIN SubmitBillByUser B ON B.WAId = ES.WAId     







 LEFT OUTER JOIN [dbo].[SubmitBillByUserAndMaterialOthersRelation] BMR ON BMR.[SubBillId] = B.SubBillId
















