CREATE View EstimateReport



AS



SELECT distinct E.EstId,Z.ZoneName AS Zone,A.AcaName AS Academy,MT.MatTypeName as MaterialType,Material.MatName AS Material,



                Unit.UnitName, ER.Qty AS [EstimateQuantity],ER.PurchaseQty AS [PurchaseQuantity],



 		        dbo.fnMaterialReceivedQuantity(ER.Sno) As ReceivedStoreQuantity, 



                (SELECT SUM(sd.DispatchQuantity) FROM StockDispatchEntry  SD where SD.EMRID=ER.Sno) AS DispatchQuantity,



				(ER.Qty - dbo.fnMaterialReceivedQuantity(ER.Sno)) AS [StorePendingQuantity],



				(ER.Qty-ER.PurchaseQty) AS [PurchaserPendingQuantity],



     			dbo.fnDirectPurchasedQuantity(ER.Sno) AS [DirectPurchasedQty],



				ER.Rate,ER.Amount,                 



                E.CreatedOn AS CreatedOnDate,convert(nvarchar(20),ER.EmployeeAssignDateTime,101) AS EmployeeAssignDate,



     			convert(nvarchar(20),ER.DispatchDate,100) AS PurchaseDate,



				(ISNULL(INC.InName,'')) AS PurchaserName,  



                CASE        



				WHEN ER.DirectPurchase=1 then 'Direct Purchased'      



                WHEN dbo.fnMaterialDispatchQuantity(ER.Sno)>=ER.Qty then 'Sent'      



                WHEN (dbo.fnMaterialDispatchQuantity(ER.Sno)!=dbo.fnMaterialReceivedQuantity(ER.Sno))  THEN 'InStore'       



				WHEN (ER.PurchaseQty>0) AND (dbo.fnMaterialReceivedQuantity(ER.Sno)=0)  THEN 'Purchased'       



                WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)!=ER.Qty) AND (dbo.fnMaterialDispatchQuantity(ER.Sno)!=ER.Qty) THEN 'Pending' 



				WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)=0) AND ER.DispatchStatus=1 AND (ISNULL(ER.DirectPurchase,0)=0) THEN 'Purchased but not received'



				END AS [StoreDispatchStatus],  



				CASE WHEN ER.DirectPurchase=1 then 'Direct Purchased'



				WHEN (ER.PurchaseQty > ER.Qty) THEN 'Over Purchased'



				WHEN (ER.PurchaseQty>0) AND (ER.Qty-ER.PurchaseQty=0)  THEN 'Purchased'



				WHEN (ER.PurchaseQty>=0) AND (ER.Qty-ER.PurchaseQty!=0)  THEN 'Pending'



				END AS [PurchaserPendingStatus],



                dbo.fnMaterialReceivedBillNumber(ER.Sno) AS 'Received BillNumber',



				ER.remarkByPurchase as Remarks,



				E.ModifyOn,



				ER.PSId,



				E.IsApproved,



				ER.PurchaseEmpID,



				ER.DispatchStatus AS [EmployeeDispatchStatus],



				E.IsReceived



FROM Estimate E     



INNER JOIN Academy A ON A.AcaId=E.AcaId      



INNER JOIN Zone Z ON Z.ZoneId=E.ZoneId      



INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId  



INNER JOIN Incharge INC ON INC.InchargeID=ER.PurchaseEmpID  



INNER JOIN Material ON ER.MatId = Material.MatId 



INNER JOIN MaterialType MT ON ER.MatTypeId = MT.MatTypeId                  



INNER JOIN Unit ON ER.UnitId = Unit.UnitId               



INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId
