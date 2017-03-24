CREATE PROCEDURE [dbo].[GetRoomStatusByBuilding]

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



	--CASE WHEN R.BuildingFloor=0 THEN 'Ground Floor'

	--	WHEN R.BuildingFloor=1 THEN 'First Floor'

	--	WHEN R.BuildingFloor=2 THEN 'Second Floor'

	--	WHEN R.BuildingFloor=3 THEN 'Third Floor'

	--	WHEN R.BuildingFloor=4 THEN 'Fourth Floor'

	--	WHEN R.BuildingFloor=5 THEN 'Fifth Floor'

	--	WHEN R.BuildingFloor=6 THEN 'Sixth Floor'

	--	ELSE R.BuildingFloor END AS BuildingFloor,

	R.BuildingFloor,

	CASE WHEN (R.IsPermanent=1) THEN 'Yes' ELSE 'No' END AS [Permanent Room],



	CASE WHEN VRN.ID IS NULL THEN 'Vacant' ELSE 'Booked' END AS RoomStatus,



	R.NumOfBed AS [Number of Bed]



	FROM RoomNumbers R



    Inner Join BuildingName B ON R.BuildingID = B.ID 



	LEFT OUTER JOIN VisitorRoomNumbers VRN ON VRN.RoomNumberID=R.ID



END



ELSE



BEGIN



SELECT Distinct



	B.Name AS [Building Name],



	R.Number AS [Room Number],



	--CASE WHEN R.BuildingFloor='0' THEN 'GroundFloor' 

	--	WHEN R.BuildingFloor='1' THEN 'FirstFloor'

	--	WHEN R.BuildingFloor='2' THEN 'SecondFloor'

	--	WHEN R.BuildingFloor='3' THEN 'ThirdFloor'

	--	WHEN R.BuildingFloor='4' THEN 'FourthFloor'

	--	WHEN R.BuildingFloor='5' THEN 'FifthFloor'

	--	WHEN R.BuildingFloor='6' THEN 'SixthFloor'

	--	ELSE R.BuildingFloor END AS [Building Floor],

	R.BuildingFloor, 

	CASE WHEN (R.IsPermanent=1) THEN 'Yes' ELSE 'No' END AS [Permanent Room],



	CASE WHEN VRN.ID IS NULL THEN 'Vacant' ELSE 'Booked' END AS RoomStatus,



	R.NumOfBed AS [Number of Bed]



	FROM RoomNumbers R



    INNER JOIN BuildingName B ON R.BuildingID = B.ID 



	LEFT OUTER JOIN VisitorRoomNumbers VRN ON VRN.RoomNumberID=R.ID



	WHERE B.ID=@BuildingID



END



END