CREATE View PurchaserEstimateReport
AS
SELECT distinct E.EstId,Z.ZoneName AS Zone,A.AcaName AS Academy,MT.MatTypeName as MaterialType,Material.MatName AS Material,
                Unit.UnitName
				, ER.Qty AS [EstimateQuantity]
				,ER.PurchaseQty AS [PurchaseQuantity]
    			,(ER.Qty-ER.PurchaseQty) AS [PurchaserPendingQuantity]
     			
				,ER.Rate
				,ER.Amount
				,ER.EmployeeAssignDateTime  AS EmployeeAssignDate
				,CONVERT(nvarchar(20),ER.DispatchDate,100) AS PurchaseDate
				,(ISNULL(INC.InName,'')) AS PurchaserName  
				,CASE WHEN ER.DirectPurchase=1 then 'Direct Purchased'
				WHEN (ER.PurchaseQty > ER.Qty) THEN 'Over Purchased'
				WHEN (ER.PurchaseQty>0) AND (ER.Qty-ER.PurchaseQty=0)  THEN 'Purchased'
				WHEN (ER.PurchaseQty>=0) AND (ER.Qty-ER.PurchaseQty!=0)  THEN 'Pending'
		    	END AS [PurchaserPendingStatus],
				ER.PSId,
    			E.IsApproved,
				ER.PurchaseEmpID,
				ER.DispatchStatus AS [EmployeeDispatchStatus],
				E.IsReceived,
   			    E.SanctionDate
				
FROM Estimate E     
INNER JOIN Academy A ON A.AcaId=E.AcaId      
INNER JOIN Zone Z ON Z.ZoneId=E.ZoneId      
INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  
INNER JOIN Incharge INC ON INC.InchargeID=ER.PurchaseEmpID  
INNER JOIN Material ON ER.MatId = Material.MatId 
INNER JOIN MaterialType MT ON ER.MatTypeId = MT.MatTypeId                  
INNER JOIN Unit ON ER.UnitId = Unit.UnitId               
INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId
