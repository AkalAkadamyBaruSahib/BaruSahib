ALTER FUNCTION [dbo].[fnMaterialReceivedQuantity]   
(     
    @EMRID INT  
)  
 

RETURNS decimal(16,2)

AS  

BEGIN  

DECLARE @receivedQuantity decimal(16,2)   

  

SELECT @receivedQuantity= SUM(sd.Quantity) FROM StockEntry  SD where SD.EMRID=@EMRID  

  

RETURN ISNULL(@receivedQuantity,0)  

END  
