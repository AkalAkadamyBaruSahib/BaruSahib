ALTER FUNCTION [dbo].[fnMaterialDispatchQuantity]  -- 13762

(     

    @EMRID INT  

)  

  

RETURNS decimal(16,2)  

AS  

BEGIN  

DECLARE @dispatchQuantity decimal(16,2)   

  

SELECT @dispatchQuantity= SUM(sd.DispatchQuantity) FROM StockDispatchEntry  SD where SD.EMRID=@EMRID  

  

RETURN ISNULL(@dispatchQuantity,0)  

END  