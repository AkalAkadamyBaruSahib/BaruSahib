ALTER PROCEDURE [dbo].[USP_NewStockReport] --2     
(  
@Firstdate datetime,
@SecondDate datetime,
@PSID INT      
)                              
AS              
BEGIN         
SELECT distinct Estimate.EstId,Z.ZoneName,A.AcaName,Material.MatName, Unit.UnitName, ER.Qty AS [EstimateQuantity],  
                ER.Rate, SE.Quantity As ReceivedQuantity,      
                (SELECT SUM(sd.DispatchQuantity) FROM StockDispatchEntry  SD where SD.EMRID=ER.Sno) AS DispatchQuantity,                    
                --SE.Rate AS ReceivedRate,             
                SE.ReceivedOn,    
                Estimate.CreatedOn,  
                (ISNULL(INC.InName,'')) AS PurchaseName,  
                CASE        
                WHEN dbo.fnMaterialDispatchQuantity(ER.Sno)>=ER.Qty then 'Sent'      
                WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)=ER.Qty) AND (dbo.fnMaterialDispatchQuantity(ER.Sno)=0)  THEN 'InStore'       
                WHEN (dbo.fnMaterialReceivedQuantity(ER.Sno)!=ER.Qty) AND (dbo.fnMaterialDispatchQuantity(ER.Sno)!=ER.Qty) THEN 'Pending' END AS [Store Status],  
                '' AS Remarks  
FROM Estimate       
INNER JOIN Academy A ON A.AcaId=Estimate.AcaId      
INNER JOIN Zone Z ON Z.ZoneId=Estimate.ZoneId      
INNER JOIN EstimateAndMaterialOthersRelations ER ON Estimate.EstId = ER.EstId  
INNER JOIN Incharge INC ON INC.InchargeID=ER.PurchaseEmpID  
INNER JOIN Material ON ER.MatId = Material.MatId               
INNER JOIN Unit ON ER.UnitId = Unit.UnitId               
INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId              
LEFT OUTER JOIN StockEntry SE ON SE.EMRID=ER.Sno           
LEFT OUTER JOIN StoreMaterialBill SMB ON SMB.EstID=Estimate.EstID AND SMB.BillNo=SE.BillPath              

WHERE ER.PSId=@PSID 
AND Estimate.IsApproved=1  
AND Estimate.ModifyOn >= @Firstdate and Estimate.ModifyOn <= @SecondDate
AND ER.DirectPurchase=0




      



END