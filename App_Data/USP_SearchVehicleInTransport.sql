CREATE procedure [dbo].[USP_SearchVehicleInTransport]      

(    

@Number as nvarchar(50),

@InchargeID INT

)         



AS



BEGIN    

DECLARE @UserType INT 

SET @UserType=14

SELECT distinct  V.*,Z.ZoneName,A.AcaName,TT.Type,     

(SELECT Name from dbo.VehicleEmployee WHERE ID=V.DriverID) AS DriverName, 

(SELECT Name from dbo.VehicleEmployee WHERE ID=V.ConductorID) AS ConducterName,

(SELECT MobileNumber from dbo.VehicleEmployee WHERE ID=V.DriverID) AS DriverNumber

,(SELECT MobileNumber from dbo.VehicleEmployee WHERE ID=V.ConductorID) AS ConducterNumber

,(SELECT COUNT(*) from dbo.VechilesNormsRelation VRN WHERE VRN.VehicleID=V.ID) AS Norms

,(SELECT Count(*) from dbo.VechilesDocumentRelation VDR WHERE VDR.VehicleID=V.ID) As DocumentCount

,(SELECT top 1 inc.InName FROM Vehicles V1 LEFT OUTER JOIN dbo.AcademyAssignToEmployee AAE on AAE.AcaID=V1.AcademyID  LEFT OUTER JOIN Incharge inc ON inc.InchargeId=AAE.EmpId WHERE INC.UserTypeID=@UserType AND V1.ID=V.ID ) AS INNAME

,(SELECT top 1  inc.InMobile FROM Vehicles V1 LEFT OUTER JOIN dbo.AcademyAssignToEmployee AAE on AAE.AcaID=V1.AcademyID  LEFT OUTER JOIN Incharge inc ON inc.InchargeId=AAE.EmpId WHERE INC.UserTypeID=@UserType AND V1.ID=V.ID ) AS TransportManagerNumber

,ISNULL((SELECT top 1 inc.InchargeID FROM Vehicles V1 LEFT OUTER JOIN dbo.AcademyAssignToEmployee AAE on AAE.AcaID=V1.AcademyID  LEFT OUTER JOIN Incharge inc ON inc.InchargeId=AAE.EmpId WHERE INC.UserTypeID=@UserType AND V1.ID=V.ID ),0) AS InchargeID    










FROM Vehicles V                  



INNER JOIN dbo.TransportTypes TT ON TT.ID=V.TypeID  

INNER JOIN [dbo].[TransportZoneAcademyRelation] TA ON TA.[TransportAcaID]=V.AcademyID    

INNER JOIN dbo.AcademyAssignToEmployee  AAE ON AAE.AcaId =V.AcademyID

INNER JOIN dbo.Zone Z ON Z.ZoneId=V.ZoneID                  

INNER JOIN dbo.Academy A ON A.AcaId=V.AcademyID  



WHERE  V.Number=@Number AND AAE.EmpID=@InchargeID





END 
















