ALTER view [dbo].[view_BillsApprovalForAdmin]            
as            
SELECT DISTINCT   
                      SubmitBillByUser.SubBillId,SubmitBillByUser.CreatedOn, 
					  CONVERT(nvarchar(20), SubmitBillByUser.BillDate, 107) AS BillDate, 
					  SubmitBillByUser.TotalAmount, SubmitBillByUser.AgencyName,   
                      Academy.AcaName, Zone.ZoneName,SubmitBillByUser.FirstVarifyStatus
FROM         SubmitBillByUser INNER JOIN  
                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN  
                     Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId 













