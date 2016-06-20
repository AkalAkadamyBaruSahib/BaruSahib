

CREATE procedure [dbo].[USP_EstimateViewForPurchaseByEmployeeID] --27,2,1           







@EmpID INT,            







@PSId INT,            







@IsApproved bit            







AS            







             







BEGIN            







SELECT DISTINCT E.SubEstimate,A.AcaID, CONVERT(nvarchar(20), E.ModifyOn, 107) AS SanctionDate,Z.ZoneName, A.AcaName, E.EstId,                 







ER.PSId,E.CreatedOn,E.ModifyOn FROM Estimate E, EstimateAndMaterialOthersRelations ER,Zone Z,Academy A                 







        







WHERE E.EstId = ER.EstId                  







and E.ZoneId = Z.ZoneId                  







and E.AcaId = A.AcaId                   







and ER.PSId = @PSId             







AND E.IsApproved=1            







AND er.PurchaseEmpID=@EmpID AND ISNULL(ER.DispatchStatus,0) !=1      







order by E.CreatedOn  desc                    







end 