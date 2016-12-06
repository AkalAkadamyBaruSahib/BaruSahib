ALTER PROCEDURE [dbo].[USP_PendingStatusForPurchaser]         
(        
@StartDate datetime,
@EndDate datetime,
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
				,([EstimateQuantity]-[PurchaseQuantity]) AS [PendingQuantity]
				,[PurchaserPendingStatus] AS [Dispatch Status]
				,Rate AS [PerItemRate]
				,Amount AS [TotalAmount]
				,CreatedOnDate
				,EmployeeAssignDate
    			,PurchaseDate
				,PurchaserName
			
              
FROM EstimateReport 















WHERE PSId=@PsId







AND IsApproved=1 







AND ISNULL(isReceived,0)=0







AND ModifyOn >= @StartDate and ModifyOn <= @EndDate















END