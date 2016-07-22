CREATE procedure [dbo].[USP_EmpLocalEstimateMaterial]      



(    



@MatName as nvarchar(50),  



@EmpID as int                



)         



AS



BEGIN



SELECT distinct A.AcaName As AcademyName,MT.MatTypeName As MaterialType,M.MatName As MaterialName,



EMR.Rate,EMR.PurchaseQty,EMR.Qty As RequiredQty,EMR.ModifyOn As PurchaseDate



FROM  Estimate E 



     INNER JOIN  EstimateAndMaterialOthersRelations EMR on E.EstId = EMR.EstId



	 INNER JOIN Zone Z on Z.ZoneId = E.ZoneId



	 INNER JOIN Academy A on A.AcaId = E.AcaId



     INNER JOIN Material M on M.MatId = EMR.MatId



	 INNER JOIN MaterialType MT on MT.MatTypeId = M.MatTypeId



WHERE EMR.DispatchStatus=1 AND EMR.PSId=1  AND M.MatName = @MatName 



AND E.CreatedBy = @EmpID



END