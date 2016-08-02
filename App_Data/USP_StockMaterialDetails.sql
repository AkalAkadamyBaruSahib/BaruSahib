CREATE PROCEDURE [dbo].[USP_StockMaterialDetails] 



(                      



@ESID INT,            



@PSID INT            



)                      



AS      



BEGIN      



SELECT distinct    Estimate.EstId,ER.Sno, Material.MatName, Unit.UnitName, ER.Qty,PurchaseSource.PSName,               



                   ER.Rate, (SELECT ISNULL(SUM(SE.Quantity),'0') FROM StockEntry  SE where SE.EMRID=ER.Sno) AS InStoreQuantity,



                   (SELECT SUM(sd.DispatchQuantity) FROM StockDispatchEntry  SD where SD.EMRID=ER.Sno) AS DispatchQuantity,            



                   ER.Rate AS ReceivedRate,     



                   ER.ModifyOn AS [ReceivedOn],     



                  -- SMB.BillPath,  



  			      SE.BillPath,



                   ER.Qty,



				  ER.MatId,



			  ER.PurchaseQty           



FROM         Estimate       



      INNER JOIN EstimateAndMaterialOthersRelations ER ON Estimate.EstId = ER.EstId       



                      INNER JOIN Material ON ER.MatId = Material.MatId       



                      INNER JOIN Unit ON ER.UnitId = Unit.UnitId       



                     INNER JOIN PurchaseSource ON ER.PSId = PurchaseSource.PSId      



                  LEFT OUTER JOIN StockEntry SE ON SE.EMRID=ER.Sno   



                   LEFT OUTER JOIN StoreMaterialBill SMB ON SMB.EstID=Estimate.EstID AND SMB.BillNo=SE.BillPath      



                     WHERE Estimate.EstId=@ESID AND ER.PSId=@PSID AND ER.DirectPurchase = 0 



END