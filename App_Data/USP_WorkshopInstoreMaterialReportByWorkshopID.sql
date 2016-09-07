CREATE procedure [dbo].[USP_WorkshopInstoreMaterialReportByWorkshopID]

(

@Acaid as varchar(50)

)         



AS                



BEGIN      



SELECT A.AcaName as WorkshopName,MA.MatName as Material,U.UnitName as Unit,MA.AkalWorkshopRate as Rate,WSM.InStoreQty      



FROM WorkshopStoreMaterial WSM    



INNER JOIN Academy A ON WSM.AcaId = A.AcaId  



INNER JOIN Material MA ON WSM.MatId = MA.MatId   



INNER JOIN Unit U ON U.UnitId = MA.UnitId  



Where WSM.AcaID in (select * from [dbo].[Split](@Acaid,',')) 





END