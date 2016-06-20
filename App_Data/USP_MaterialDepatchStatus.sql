CREATE procedure [dbo].[USP_MaterialDepatchStatus] --'dhotian'            







(                  







  @UserId INT,

  

  @PSID INT                 







)                  







AS                    







BEGIN        







                    







SELECT DISTINCT Estimate.SubEstimate,          







CONVERT(nvarchar(20), Estimate.ModifyOn, 107) as SanctionDate,Estimate.CreatedOn,Academy.AcaID, Academy.AcaName,Z.ZoneName, Estimate.EstId, Incharge.LoginId,Estimate.ModifyOn                  







FROM         Estimate             







INNER JOIN EstimateAndMaterialOthersRelations ON Estimate.EstId = EstimateAndMaterialOthersRelations.EstId             







INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId        







INNER JOIN Zone Z ON Z.ZoneID=Estimate.ZoneID        







INNER JOIN AcademyAssignToEmployee ON Estimate.AcaID = AcademyAssignToEmployee.AcaID             







INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId    







    







WHERE Incharge.InchargeId=@UserId  AND  Estimate.IsApproved=1 AND EstimateAndMaterialOthersRelations.PSId=@PSID  







order by Estimate.CreatedOn                          







END