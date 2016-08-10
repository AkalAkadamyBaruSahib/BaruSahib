CREATE PROCEDURE [dbo].[USP_NewDispatchExcelForPurchaserAndStore]            



(        



@Firstdate datetime,



@SecondDate datetime,      



@PsId as int              



)              



AS                



BEGIN      



SELECT distinct * from EstimateReport WHERE PSId=@PsId 

AND IsApproved=1  



AND ModifyOn >= @Firstdate and ModifyOn <= @SecondDate



END
