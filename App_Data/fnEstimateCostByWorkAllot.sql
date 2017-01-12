



ALTER FUNCTION [dbo].[fnEstimateCostByWorkAllot] 
(     
    @WaID INT,
	@PSid INT
)  
RETURNS decimal(16,2)  
AS  
BEGIN  
DECLARE @estimateQuantity decimal(16,2)   
SELECT distinct @estimateQuantity= SUM(distinct E.EstmateCost) FROM Estimate E 
Inner JOIn EstimateAndMaterialOthersRelations es on es.EstId=E.EstId
where E.WAId=@WaID AND es.PSId=@PSid AND E.IsApproved=1
RETURN ISNULL(@estimateQuantity,0)  
END 