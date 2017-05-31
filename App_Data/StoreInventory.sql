CREATE View StoreInventory

AS

SELECT distinct E.EstId,Z.ZoneName AS Zone,A.AcaName AS Academy,MT.MatTypeName as MaterialType,Material.MatName AS Material,Unit.UnitName, 

				ER.Qty AS [EstimateQuantity],

				dbo.fnMaterialReceivedQuantity(ER.Sno) As StoreReceivedQuantity, 

				dbo.fnMaterialDispatchQuantity(ER.Sno) As StoreDispatchQuantity, 

				(ER.Qty - dbo.fnMaterialReceivedQuantity(ER.Sno)) AS [PendingQuantity],

				CASE        

				WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)>0 AND dbo.fnMaterialDispatchQuantity(ER.Sno)!=dbo.fnMaterialReceivedQuantity(ER.Sno))  THEN 'InStore'       

				WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)!=ER.Qty) THEN 'Pending' 

				WHEN (dbo.fnMaterialDispatchQuantity(ER.Sno)>=ER.Qty) THEN 'Sent' 

				END AS [StoreStatus],  

				dbo.fnMaterialReceivedBillNumber(ER.Sno) AS 'Received BillNumber',

				ER.PSId,

    			E.IsApproved,

				E.SanctionDate,

				E.IsReceived

FROM Estimate E



INNER JOIN Academy A ON A.AcaId=E.AcaId      

INNER JOIN Zone Z ON Z.ZoneId=E.ZoneId      

INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  

INNER JOIN Material ON ER.MatId = Material.MatId 

INNER JOIN MaterialType MT ON ER.MatTypeId = MT.MatTypeId                  

INNER JOIN Unit ON ER.UnitId = Unit.UnitId               

INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId