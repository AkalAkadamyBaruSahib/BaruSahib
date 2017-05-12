ALTER PROCEDURE [dbo].[GetDailyWiseVisitorsReports] --'1/1/0001 00:00:00:000','12/31/2015 00:00:00:000'
(                          
@from datetime
)                          
AS          
BEGIN     
SELECT distinct V.Name,

                V.VisitorAddress,

            	CO.CountryName,

             	S.StateName,

            	C.CityName, 

            	V.ContactNumber,

             	B.Name AS BuildingName,

             	R.Number AS RoomNumber,

                V.RoomRent AS LangerSewa,

              	V.PurposeOfVisit,

            	V.AdmissionNumber,

            	V.TotalNoOfMen,

            	V.TotalNoOfWomen,

            	V.TotalNoOfChildren,

                V.VehicleNo,

            	V.TimePeriodTo As CheckOut

FROM Visitors V 

INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID 

INNER JOIN Country CO ON CO.CountryId = V.Country

INNER JOIN State S ON S.StateId = V.State

INNER JOIN City C ON C.CityId = V.City

INNER JOIN BuildingName B ON B.ID = V.BuildingID

INNER JOIN VisitorRoomNumbers VRN ON VRN.VisitorID = V.ID

INNER JOIN RoomNumbers R ON R.ID = VRN.RoomNumberID

WHERE V.TimePeriodFrom >= @from AND V.VisitorTypeID=1

END

