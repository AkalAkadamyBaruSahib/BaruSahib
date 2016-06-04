ALTER procedure [dbo].[USP_GetVehiclesDetailsByInchargeID]   

(

@isApproved BIT,

@InchargeId int

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



,inc.InName AS INNAME



,inc.InMobile AS TransportManagerNumber



,inc.InchargeID AS InchargeID    





FROM Vehicles V                  



INNER JOIN dbo.TransportTypes TT ON TT.ID=V.TypeID  



INNER JOIN [dbo].[TransportZoneAcademyRelation] TA ON TA.[TransportAcaID]=V.AcademyID    



INNER JOIN dbo.Zone Z ON Z.ZoneId=V.ZoneID                  



INNER JOIN dbo.Academy A ON A.AcaId=V.AcademyID    

INNER JOIN   AcademyAssignToEmployee AAE on AAE.AcaId = TA.TransportAcaID

INNER JOIN Incharge inc on inc.InchargeId = AAE.EmpId 



WHERE V.IsApproved=@IsApproved and inc.InchargeId = @InchargeId











END 