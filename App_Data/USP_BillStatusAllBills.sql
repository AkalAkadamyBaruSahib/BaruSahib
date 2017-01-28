ALTER procedure [dbo].[USP_BillStatusAllBills]          

            

 as             

begin                

SELECT DISTINCT       

                      SubmitBillByUser.SubBillId, SubmitBillByUser.AgencyName AS AgencyName, SubmitBillByUser.TotalAmount, SubmitBillByUser.FirstVarify AS HQ, CONVERT(nvarchar(20),       

                      SubmitBillByUser.FirstVarifyOn, 100) AS HQVarifyDate, SubmitBillByUser.FirstVarifyRemark AS HQRemark, SubmitBillByUser.SeccondVarify AS Audit,       

                      CONVERT(nvarchar(20), SubmitBillByUser.SeccondVarifyOn, 100) AS AuditVarifyDate, SubmitBillByUser.SecondVarifyRemark AS AuditRemark,       

                      SubmitBillByUser.FirstVarifyStatus AS HQStatus, SubmitBillByUser.SecondVarifyStatus AS AuditStatus, SubmitBillByUser.PaymentStatus AS AccStatus,       

                      Academy.AcaName, Academy.AcId, Academy.AcaId, CONVERT(nvarchar(20), SubmitBillByUser.DateOfReceving, 100) AS DateOfRecevi,       

                      SubmitBillByUser.RecevingRemark, SubmitBillByUser.RecevingStatus, SubmitBillByUser.ThirdVarifyBy, SubmitBillByUser.ThirdVarifyRemark,       

                      CONVERT(nvarchar(20), SubmitBillByUser.ThirdVarifyOn, 100) AS AccVarifyDate, SubmitBillByUser.CreatedOn, Zone.ZoneName      

FROM         SubmitBillByUser INNER JOIN      

                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId  INNER JOIN      

                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId  order by SubmitBillByUser.SubBillId  desc 

                                      

end
