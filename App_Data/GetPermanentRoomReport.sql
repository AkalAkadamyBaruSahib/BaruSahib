ALTER PROCEDURE [dbo].[GetPermanentRoomReport]

(                          

@BuildingID INT

)                          

AS          

BEGIN     

IF(@BuildingID=-1)

BEGIN

	SELECT distinct V.Name,

	VT.VisitorType As [Employee Type],

	V.ContactNumber,

	V.VisitorAddress As [Employee Address],

	B.Name As BuildingName,

	RN.BuildingFloor AS BuildingFloor,

	RN.Number As RoomNumber,

    CASE WHEN V.RoomRentType=1 THEN 'Paid By Trust' ELSE 'SELF' END AS [Room Rent],

	CASE WHEN V.ElectricityBill=1 THEN 'Paid By Trust' ELSE 'SELF' END AS [Electricity Bill],

	V.Identification

	FROM Visitors V

	INNER jOIN VisitorRoomNumbers RM ON RM.VisitorID=V.ID

	INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID  

	INNER JOIN BuildingName B ON B.ID= V.BuildingID 

	INNER JOIN RoomNumbers RN ON RM.RoomNumberID = RN.ID 

	WHERE V.VisitorTypeID !=1

END

ELSE

BEGIN

SELECT distinct V.Name,

	VT.VisitorType As [Employee Type],

	V.ContactNumber,

	V.VisitorAddress As [Employee Address],

	B.Name As BuildingName,

	RN.BuildingFloor AS BuildingFloor,

	RN.Number As RoomNumber,

    CASE WHEN V.RoomRentType=1 THEN 'Paid By Trust' ELSE 'SELF' END AS [Room Rent],

	CASE WHEN V.ElectricityBill=1 THEN 'Paid By Trust' ELSE 'SELF' END AS [Electricity Bill],

	V.Identification

	FROM Visitors V

	INNER jOIN VisitorRoomNumbers RM ON RM.VisitorID=V.ID

	INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID  

	INNER JOIN BuildingName B ON B.ID= V.BuildingID 

	INNER JOIN RoomNumbers RN ON RM.RoomNumberID = RN.ID 

	WHERE V.VisitorTypeID !=1 AND B.ID=@BuildingID

END

END
