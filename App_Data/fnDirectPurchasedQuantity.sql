ALTER FUNCTION [dbo].[fnDirectPurchasedQuantity]   
(     
    @EMRID INT
)  

RETURNS decimal(16,2)
AS  
BEGIN  
DECLARE @directQuantity decimal(16,2)=0
DECLARE @storeReceivedQuantity decimal(16,2)=0
DECLARE @PurchaseQTY decimal(16,2)=0

SELECT @storeReceivedQuantity= SUM(sd.Quantity) FROM StockEntry  SD where SD.EMRID=@EMRID  
SELECT @PurchaseQTY= PurchaseQty FROM EstimateAndMaterialOthersRelations where Sno=@EMRID  AND DirectPurchase=1

SET @directQuantity = (@PurchaseQTY-@storeReceivedQuantity)

RETURN ISNULL(@directQuantity,0)  

END