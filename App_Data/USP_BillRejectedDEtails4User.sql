ALTER procedure [dbo].[USP_BillRejectedDEtails4User]                  

(                  

@UserID as int           

)                  

as                  

begin                  

SELECT     SubmitBillByUser.SubBillId, SubmitBillByUser.AgencyName, SubmitBillByUser.TotalAmount, SubmitBillByUser.FirstVarify AS HQ, CONVERT(nvarchar(20),       

                      SubmitBillByUser.FirstVarifyOn, 100) AS HQVarifyDate, SubmitBillByUser.FirstVarifyRemark AS HQRemark, SubmitBillByUser.SeccondVarify AS Audit,       

                      CONVERT(nvarchar(20), SubmitBillByUser.SeccondVarifyOn, 100) AS AuditVarifyDate, SubmitBillByUser.SecondVarifyRemark AS AuditRemark,       

                      SubmitBillByUser.FirstVarifyStatus AS HQStatus, SubmitBillByUser.SecondVarifyStatus AS AuditStatus, SubmitBillByUser.PaymentStatus AS AccStatus,       

                      SubmitBillByUser.ZoneId, Academy.AcaName, Academy.AcId, Academy.AcaId, CONVERT(nvarchar(20), SubmitBillByUser.DateOfReceving, 100) AS DateOfRecevi,       

                      SubmitBillByUser.RecevingRemark, SubmitBillByUser.RecevingStatus, SubmitBillByUser.ThirdVarifyBy, SubmitBillByUser.ThirdVarifyRemark,       

                      CONVERT(nvarchar(20), SubmitBillByUser.ThirdVarifyOn, 100) AS AccVarifyDate, BillProceedLog.UserProStatus      

FROM         SubmitBillByUser INNER JOIN      

                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId LEFT OUTER JOIN      

                      BillProceedLog ON SubmitBillByUser.SubBillId = BillProceedLog.BillId   

                      where  SubmitBillByUser.FirstVarifyStatus=0  or SubmitBillByUser.SecondVarifyStatus=0 or SubmitBillByUser.PaymentStatus=0 or SubmitBillByUser.RecevingStatus=0  and   

      SubmitBillByUser.AcaID = (select distinct AcaID from AcademyAssignToEmployee where Active=1 and EmpId=(select InchargeId from Incharge where InchargeId=@UserID))  

                                        

end
