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
				,[PendingQuantity]
    			,Rate
				,Amount
				,CreatedOnDate
    			,EmployeeAssignDate
				,PurchaseDate
				,PurchaserName,  
				[Dispatch Status],  
                [Received BillNumber],
				[EmployeeDispatchStatus]



				 from EstimateReport WHERE PSId=@PsId 



AND IsApproved=1  





AND ModifyOn >= @Firstdate and ModifyOn <= @SecondDate







END
