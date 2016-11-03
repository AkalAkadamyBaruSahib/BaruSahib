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

	V.PurposeOfVisit,

	B.Name,

	V.RoomRent,

	V.AdmissionNumber,

	V.VisitorReference,

	V.TimePeriodFrom As CheckIn,

	V.TimePeriodTo As CheckOut

	FROM Visitors V

	INNER jOIN VisitorRoomNumbers RM ON RM.VisitorID=V.ID

	INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID  

	INNER JOIN BuildingName B ON B.ID= V.BuildingID 

	LEFT OUTER JOIN RoomNumbers R ON R.BuildingID = B.ID

	where V.TimePeriodFrom >= @from and V.TimePeriodTo <= @To 

END

END




