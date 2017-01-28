ALTER procedure [dbo].[USP_AllBillDetails]      



as      



begin      



SELECT     SubmitBillByUser.SubBillId, CONVERT (nvarchar(20), SubmitBillByUser.BillDate, 107) as BillDate, VendorInfo.VendorName AS AgencyName, SubmitBillByUser.TotalAmount,       



                      SubmitBillByUser.EstId, SubmitBillByUser.BillType, Academy.AcaName, Zone.ZoneName, BillType.BillTypeName, WorkAllot.WorkAllotName      



FROM         SubmitBillByUser INNER JOIN      



                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN 
					  
					   VendorInfo ON SubmitBillByUser.VendorID = VendorInfo.ID INNER JOIN     



                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId LEFT OUTER JOIN      



                      BillType ON SubmitBillByUser.ChargetoBillTyId = BillType.BillTypeId LEFT OUTER JOIN  



                      WorkAllot ON SubmitBillByUser.WAId = WorkAllot.WAId order by SubmitBillByUser.SubBillId desc



end
