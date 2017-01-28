ALTER procedure   [dbo].[USP_AllBillDetailsByAcaId]      

(      

@AcaId as int      

)        

as        

begin        

SELECT     SubmitBillByUser.SubBillId, CONVERT (nvarchar(20), SubmitBillByUser.BillDate, 107) as BillDate, SubmitBillByUser.AgencyName, SubmitBillByUser.TotalAmount,         

                      SubmitBillByUser.EstId, SubmitBillByUser.BillType, Academy.AcaName, Zone.ZoneName,WorkAllot.WorkAllotName        

FROM         SubmitBillByUser INNER JOIN        

                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN        

                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId LEFT OUTER JOIN        

                      WorkAllot ON SubmitBillByUser.WAId = WorkAllot.WAId where  SubmitBillByUser.AcaId=@AcaId order by     SubmitBillByUser.SubBillId desc

                      --and  SubmitBillByUser.PaymentStatus is not null       

end
