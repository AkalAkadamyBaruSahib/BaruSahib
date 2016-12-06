ALTER PROCEDURE [dbo].[USP_NewDispatchExcelForPurchaserAndStore]            
(        
@Firstdate datetime,
@SecondDate datetime,      
@PsId as int              
)              
AS                
BEGIN      
SELECT distinct  EstId
				,Zone
				,Academy
				,MaterialType
				,Material
				,UnitName
    			,[EstimateQuantity]
				,[PurchaseQuantity]
				,ReceivedStoreQuantity
				,DispatchQuantity
				,[StorePendingQuantity]
				,[DirectPurchasedQty]
     			,Rate AS [PerItemRate]
				,Amount AS [TotalAmount]
				,CreatedOnDate
    			,EmployeeAssignDate
				,PurchaseDate
				,PurchaserName
				,[StoreDispatchStatus] AS [Dispatch Status],  
                [Received BillNumber],
				[EmployeeDispatchStatus]
         		 from EstimateReport WHERE PSId=@PsId 
AND IsApproved=1  AND IsReceived=0



AND ModifyOn >= @Firstdate and ModifyOn <= @SecondDate



END
