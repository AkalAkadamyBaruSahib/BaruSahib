ALTER PROCEDURE [dbo].[GetDailyWiseVisitorsReports] --'5/11/2017'
(                          
@from datetime
)                          
AS          
BEGIN     
SELECT distinct V.TimePeriodFrom As CheckIn,
                V.Name,
                V.VisitorAddress,
            	CO.CountryName,
             	S.StateName,
            	C.CityName, 
            	V.ContactNumber,
             	B.Name AS BuildingName,
             	R.Number AS BookedRoomNumber,
				RO.Number AS CheckOutRoomNumber,
                V.RoomRent AS LangerSewa,
              	V.PurposeOfVisit,
            	V.AdmissionNumber,
            	V.TotalNoOfMen,
            	V.TotalNoOfWomen,
            	V.TotalNoOfChildren,
                V.VehicleNo,
				V.Identification,
				V.VisitorReference AS ReferenceTo,
           	    V.TimePeriodTo As CheckOut
FROM Visitors V 
INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID 
INNER JOIN Country CO ON CO.CountryId = V.Country
INNER JOIN State S ON S.StateId = V.State
INNER JOIN City C ON C.CityId = V.City
INNER JOIN BuildingName B ON B.ID = V.BuildingID
LEFT OUTER JOIN VisitorRoomNumbers VRN ON VRN.VisitorID = V.ID
LEFT OUTER  JOIN RoomNumbers R ON R.ID = VRN.RoomNumberID
LEFT OUTER  JOIN VisitorsCheckOutLog VCL ON VCL.VisitorID = V.ID
LEFT OUTER  JOIN RoomNumbers RO ON RO.ID = VCL.RoomNumberID
WHERE V.TimePeriodFrom >= @from AND V.VisitorTypeID=1
END












