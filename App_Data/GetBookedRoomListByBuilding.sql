CREATE PROCEDURE [dbo].[GetBookedRoomListByBuilding] 

(                          

@BuildingID INT

)                          

AS          

BEGIN     

IF(@BuildingID=-1)

BEGIN

SELECT Distinct

	B.Name AS [Building Name],

	R.Number AS [Room Number],

	R.BuildingFloor,

	CASE WHEN (R.IsPermanent=1) THEN 'Yes' ELSE 'No' END AS [Permanent Room],

	R.NumOfBed AS [Number of Bed]

	FROM RoomNumbers R

    Inner Join BuildingName B ON R.BuildingID = B.ID 

WHERE R.ID IN (SELECT RoomNumberID from VisitorRoomNumbers)



END

ELSE

BEGIN

SELECT Distinct

	B.Name AS [Building Name],

	R.Number AS [Room Number],

	R.BuildingFloor,

	CASE WHEN (R.IsPermanent=1) THEN 'Yes' ELSE 'No' END AS [Permanent Room],

	R.NumOfBed AS [Number of Bed]

	FROM RoomNumbers R

    Inner Join BuildingName B ON R.BuildingID = B.ID 

	WHERE R.ID IN (SELECT RoomNumberID from VisitorRoomNumbers)	AND B.ID=@BuildingID



END

END