CREATE PROCEDURE [dbo].[USP_NewDispatchExcelForPurchaserAndStore]            

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

     			,SanctionDate

    			,EmployeeAssignDate

				,PurchaseDate

				,PurchaserName

				,StoreReceivedOn

				,StoreDispatchOn

				,[StoreDispatchStatus] AS [Dispatch Status],  

                [Received BillNumber],

				[EmployeeDispatchStatus]

         		 from EstimateReport WHERE PSId=@PsId 

AND IsApproved=1  AND IsReceived=0

AND  SanctionDate >= @Firstdate and SanctionDate <= @SecondDate

END
