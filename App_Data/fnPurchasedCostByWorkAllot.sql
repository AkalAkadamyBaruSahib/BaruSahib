



ALTER FUNCTION [dbo].[fnPurchasedCostByWorkAllot] 



(     



    @WaID INT  



)  







RETURNS decimal(16,2)  



AS  



BEGIN  







DECLARE @purchasedCost decimal(16,2)   



SELECT distinct @purchasedCost= SUM(distinct E.TotalAmount) FROM SubmitBillByUser E where E.WAId=@WaID



RETURN ISNULL(@purchasedCost,0)  















END 