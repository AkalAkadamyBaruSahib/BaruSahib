ALTER PROCEDURE [dbo].[GetVisitorsReports] --'1/1/0001 00:00:00:000','12/31/2015 00:00:00:000'
(                          
@from datetime,
@To datetime
)                          
AS          
BEGIN     
IF(@from!='' AND  @To!='')
BEGIN
	SELECT distinct V.Name,
	VT.VisitorType,
	V.ContactNumber,
	V.VisitorAddress,
	CO.CountryName,
	S.StateName,
	C.CityName,
	V.PurposeOfVisit,
	B.Name As BuildingName,
	RN.BuildingFloor AS BuildingFloor,
	RN.Number As RoomNumber,
    V.RoomRent,
    V.AdmissionNumber,
	V.VisitorReference,
	V.VehicleNo,
	V.Identification,
	V.TimePeriodFrom As CheckIn,
	V.TimePeriodTo As CheckOut
	FROM Visitors V
	INNER jOIN VisitorRoomNumbers RM ON RM.VisitorID=V.ID
	INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID  
	INNER JOIN BuildingName B ON B.ID= V.BuildingID 
	INNER JOIN RoomNumbers RN ON RM.RoomNumberID = RN.ID 
	INNER JOIN Country CO ON CO.CountryId = V.Country
	INNER JOIN State S ON S.StateId = V.State
	INNER JOIN City C ON C.CityId = V.City
	where V.TimePeriodFrom >= @from and V.TimePeriodTo <= @To 



END



END