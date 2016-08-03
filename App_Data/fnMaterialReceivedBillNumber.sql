ALTER FUNCTION [dbo].[fnMaterialReceivedBillNumber]   
(     
   @EMRID INT  
)  

RETURNS INT  

AS  

BEGIN  

DECLARE @billnumber varchar(20) = null

SELECT @billnumber= COALESCE(@billnumber + ',', '' ) + CAST(b.BillNo AS varchar)  FROM StockEntry  SE 
INNER JOIN StoreMaterialbill b on b.ID=SE.BillPath

WHERE SE.EMRID=@EMRID


RETURN ISNULL(@billnumber,'Material Not Received')  

END  