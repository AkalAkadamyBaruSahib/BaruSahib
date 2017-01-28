ALTER proc GetBillDetailsByVendorID 
(
@VendorID int
)
AS
BEgin
select V.VendorName,S.SubBillId,W.WorkAllotName,S.TotalAmount,M.MatName,Inc.InName,S.CreatedOn from VendorInfo V
INNER JOIN SubmitBillByUser S ON S.VendorID = V.ID
INNER JOIN WorkAllot W ON S.WAId = W.WAId
LEFT OUTER JOIN SubmitBillByUserAndMaterialOthersRelation SMB ON SMB.SubBillId = S.SubBillId
INNER JOIN Material M on M.MatId = SMB.MatId
INNER JOIN Incharge Inc on Inc.InchargeId= S.CreatedBy
WHERE V.ID=@VendorID
END


