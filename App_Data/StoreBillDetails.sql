CREATE PROCEDURE [dbo].[USP_StoreBillDetails] 
(                      
@ESID INT,            
@EMRID INT            
)                      
AS      
BEGIN      
  select SE.ReceivedOn, SMB.BillPath, SE.ID

FROM 
StockEntry SE 
INNER JOIN  StoreMaterialBill SMB ON SMB.BillNo=SE.BillPath  

WHERE SE.EMRID=@EMRID AND SMB.EstID=@ESID

END