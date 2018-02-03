CREATE PROCEDURE [dbo].GetNonApprovedVehicleInfo    

(  

   @ZoneId as int  

)   

AS    

BEGIN    

SELECT DISTINCT Aca.AcaName AS [Academy Name],V.number AS [Vehicle Number],T.Type AS [Transport Type]

FROM Vehicles V

INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID     

INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID     

INNER JOIN Academy Aca ON Aca.AcaID = A.AcaID   

INNER JOIN TransportTypes T ON T.ID =V.TypeID

WHERE INC.ModuleID=2 and V.IsApproved=0 and V.ZoneID=@ZoneId















END