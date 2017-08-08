ALTER view [dbo].[view_BillsApprovalForAdmin]            

AS            

SELECT DISTINCT S.SubBillId,

S.CreatedOn, 

CONVERT(nvarchar(20), S.BillDate, 107) AS BillDate, 

S.TotalAmount,

 S.AgencyName,   

Academy.AcaName, 

Zone.ZoneName,

S.FirstVarifyStatus,

S.AcaId,

S.SecondVarifyStatus, 

S.PaymentStatus,

S.RecevingStatus,

S.EstId,

S.BillType,

BillType.BillTypeName,

WorkAllot.WorkAllotName

FROM  SubmitBillByUser  S INNER JOIN  

Academy ON S.AcaId = Academy.AcaId INNER JOIN  

Zone ON S.ZoneId = Zone.ZoneId  LEFT OUTER JOIN 

BillType ON S.ChargetoBillTyId = BillType.BillTypeId LEFT OUTER JOIN 
 
WorkAllot ON S.WAId = WorkAllot.WAId