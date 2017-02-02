ALTER FUNCTION [dbo].[fnPurchasedCostByWorkAllot] 

(     

    @WaID INT  

)  

RETURNS decimal(16,2)  

AS  

BEGIN  

DECLARE @purchasedCost decimal(16,2)   

SELECT distinct @purchasedCost= SUM(SE.Amount) FROM SubmitBillByUser S 
INNER JOIN SubmitBillByUserAndMaterialOthersRelation SE ON S.SubBillId = SE.SubBillId 
WHERE S.WAId=@WaID AND S.IsApproved =1

RETURN ISNULL(@purchasedCost,0)  

END 
