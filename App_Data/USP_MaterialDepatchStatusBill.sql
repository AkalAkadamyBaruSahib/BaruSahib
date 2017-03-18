CREATE procedure [dbo].[USP_MaterialDepatchStatusBill]  



   (  



   @EstId as int  



    )      



as            



begin            



SELECT DISTINCT Estimate.SubEstimate,Z.ZoneName, CONVERT(nvarchar(20), Estimate.SanctionDate, 107) AS SanctionDate, Academy.AcaName, Estimate.EstId,Incharge.InName,Incharge.InMobile    



FROM         Estimate INNER JOIN    



                      EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId INNER JOIN    



                      Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN



                      Zone Z ON Z.ZoneID= Estimate.ZoneId INNER JOIN



                      AcademyAssignToEmployee ON Estimate.ZoneId = AcademyAssignToEmployee.ZoneId INNER JOIN



					  Incharge ON Incharge.InchargeId  = Estimate.ModifyBy   where Estimate.EstId=@EstId  



end 