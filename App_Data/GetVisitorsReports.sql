CREATE PROCEDURE [dbo].[GetVisitorsReports] --'1/1/0001 00:00:00:000','12/31/2015 00:00:00:000'

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

	B.Name AS BuildingName,

	V.PurposeOfVisit,

    V.RoomRent AS LangerSewa,

    V.AdmissionNumber,

    V.VisitorReference,

	V.VehicleNo,

    V.Identification,

	V.CreatedOn As CheckIn,

	V.TimePeriodTo As CheckOut

	FROM Visitors V

	INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID  

	INNER JOIN Country CO ON CO.CountryId = V.Country

	INNER JOIN State S ON S.StateId = V.State

	INNER JOIN City C ON C.CityId = V.City

	INNER JOIN BuildingName B ON B.ID = V.BuildingID

	WHERE V.TimePeriodFrom >= @from and V.TimePeriodTo <= @To AND V.VisitorTypeID=1

END

END
