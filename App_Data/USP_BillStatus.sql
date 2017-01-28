ALTER procedure [dbo].[USP_BillStatus]  
as  
begin  
SELECT     SubmitBillByUser.SubBillId, Zone.ZoneName, Academy.AcaName,
          CASE SubmitBillByUser.BillType WHEN 1 THEN 'Sanctioned' WHEN 2 THEN 'Non Sanctioned'  END AS BillType, 
          SubmitBillByUser.AgencyName, SubmitBillByUser.TotalAmount, 
           Incharge.InName AS HQ, CONVERT(nvarchar(20), SubmitBillByUser.FirstVarifyOn, 100) AS HQVarifyDate, 
           SubmitBillByUser.FirstVarifyRemark AS HQRemark, 
           CASE FirstVarifyStatus WHEN 0 THEN 'Reject' WHEN 1 THEN 'VERIFY' WHEN 2 THEN 'PROCEED' ELSE 'PENDING' END AS HQStatus, 
           Inc.InName AS Audit, CONVERT(nvarchar(20), SubmitBillByUser.SeccondVarifyOn, 100) AS AuditVarifyDate, 
            SubmitBillByUser.SecondVarifyRemark AS AuditRemark, 
            CASE SecondVarifyStatus WHEN 0 THEN 'REJECT' WHEN 1 THEN 'VERIFY' WHEN 2 THEN 'PROCEED' ELSE 'PENDING' END AS AuditStatus, 
           Inch.InName AS Account, SubmitBillByUser.ThirdVarifyRemark AS AccountRemark, CONVERT(nvarchar(20), SubmitBillByUser.ThirdVarifyOn, 100) 
           AS AccVarifyDate, CASE PaymentStatus WHEN 0 THEN 'REJECT' WHEN 1 THEN 'VERIFY' WHEN 2 THEN 'PROCEED' ELSE 'PENDING' END AS AccStatus, 
           CONVERT(nvarchar(20), SubmitBillByUser.DateOfReceving, 100) AS DateOfRecevi, SubmitBillByUser.RecevingRemark, 
          CASE RecevingStatus WHEN 0 THEN 'REJECT' WHEN 1 THEN 'VERIFY' WHEN 2 THEN 'PROCEED' ELSE 'PENDING' END AS ReccStatus, 

                 InchCreted.InName AS BillSubmitedBy







FROM         SubmitBillByUser INNER JOIN







                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId  INNER JOIN


					  Incharge InchCreted  ON SubmitBillByUser.CreatedBy = InchCreted.InchargeId INNER JOIN





                      Zone ON SubmitBillByUser.ZoneId = Zone.ZoneId LEFT OUTER JOIN







					  Incharge ON SubmitBillByUser.FirstVarify = Incharge.InchargeId LEFT OUTER JOIN







				      Incharge Inc ON SubmitBillByUser.SeccondVarify = Inc.InchargeId LEFT OUTER JOIN







				      Incharge Inch  ON SubmitBillByUser.ThirdVarifyBy = Inch.InchargeId 

					 







end
