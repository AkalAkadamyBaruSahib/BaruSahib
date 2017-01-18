CREATE PROCEDURE [dbo].[USP_MsgContent]







(







@BillId as int







)







as







begin







SELECT     SubmitBillByUser.BillType, SubmitBillByUser.SubBillId, Academy.AcaName, Incharge.InName







FROM         SubmitBillByUser INNER JOIN







                      Academy ON SubmitBillByUser.AcaId = Academy.AcaId INNER JOIN







                      Incharge ON SubmitBillByUser.CreatedBy  = Incharge.InchargeId    where SubmitBillByUser.SubBillId=@BillId







end