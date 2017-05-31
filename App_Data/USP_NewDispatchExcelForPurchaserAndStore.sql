CREATE PROCEDURE [dbo].[USP_NewDispatchExcelForPurchaserAndStore]            

(        

@Firstdate datetime,

@SecondDate datetime,      

@PsId as int              

)              

AS                

BEGIN      

SELECT 

EstId

,Zone

,Academy

,MaterialType

,Material

,UnitName

,EstimateQuantity

,StoreReceivedQuantity

,StoreDispatchQuantity

,PendingQuantity

,StoreStatus

,[Received BillNumber]

,SanctionDate AS EstimateReceivedOn



 FROM StoreInventory

WHERE PSId=@PsId 

AND IsApproved=1  AND IsReceived=0

AND  SanctionDate >= @Firstdate and SanctionDate <= @SecondDate

END
