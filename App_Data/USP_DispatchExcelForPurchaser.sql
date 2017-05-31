ALTER PROCEDURE [dbo].[USP_DispatchExcelForPurchaser]         
(        
@StartDate datetime,
@EndDate datetime,
@PsId as int,
@PurchaseEmpID as int =0
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
				,[PurchaserPendingQuantity]
				
				,Rate AS [PerItemRate]
				,Amount AS [TotalAmount]
	            ,SanctionDate AS EstimateReceivedOn
				,EmployeeAssignDate
    			,PurchaseDate
  				,PurchaserName
    			,[PurchaserPendingStatus] AS [Dispatch Status]
FROM PurchaserEstimateReport 
WHERE PSId=@PsId
AND IsApproved=1 
AND ISNULL([EmployeeDispatchStatus],0)=0
AND ((PurchaseEmpID = @PurchaseEmpID) AND (@PurchaseEmpID=@PurchaseEmpID)) AND SanctionDate >= @StartDate and SanctionDate <= @EndDate
END








