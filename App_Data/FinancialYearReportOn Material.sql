Select E.EstID,Z.ZoneName,A.AcaName As Academy,MT.MatTypeName As MaterialType,M.MatName As MaterialName,U.UnitName,ER.Qty As Quantity,ER.Rate,(ER.Qty*ER.Rate) as TotalAmount,
datename(M,E.ModifyOn)+' '+cast(datepart(YYYY,E.ModifyOn) as varchar) as [Date]
FROM Estimate E
INNER JOIN EstimateAndMaterialOthersRelations ER ON ER.Estid=E.Estid
INNER JOIN MaterialType MT ON MT.MatTypeId=ER.MatTypeId
INNER JOIN Material M ON M.MatId=ER.MatId
INNER JOIN Unit U ON U.UnitId=ER.UnitId
INNER JOIN Academy A ON E.AcaId=A.AcaId
INNER JOIN Zone Z ON Z.ZoneId=E.ZoneId

WHERE e.modifyon > '04/01/2016' and e.modifyon <= '03/31/2017' and E.IsApproved=1 and E.IsActive=1 and ER.DispatchStatus=1 and ER.PSID=2 
and (MT.MatTypeId = 22 or  MT.MatTypeId = 35) Order By E.ModifyOn

