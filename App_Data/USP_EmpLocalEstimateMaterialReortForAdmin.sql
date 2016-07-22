ALTER procedure [dbo].[USP_EmpLocalEstimateMaterialReortForAdmin]      
(    
@Firstdate datetime,
@SecondDate datetime              
)         
AS
BEGIN
SELECT distinct Z.ZoneName,A.AcaName As AcademyName,MT.MatTypeName As MaterialType,M.MatName As MaterialName,
EMR.Rate,EMR.PurchaseQty,EMR.Qty As QuantityInEstimate,EMR.ModifyOn As PurchaseDate
FROM  Estimate E 
     INNER JOIN  EstimateAndMaterialOthersRelations EMR on E.EstId = EMR.EstId
     INNER JOIN Zone Z on Z.ZoneId = E.ZoneId
	 INNER JOIN Academy A on A.AcaId = E.AcaId
	 INNER JOIN Material M on M.MatId = EMR.MatId
	 INNER JOIN MaterialType MT on MT.MatTypeId = M.MatTypeId
WHERE EMR.DispatchStatus=1 AND EMR.PSId=1  AND EMR.ModifyOn >= @Firstdate and EMR.ModifyOn <= @SecondDate 
END