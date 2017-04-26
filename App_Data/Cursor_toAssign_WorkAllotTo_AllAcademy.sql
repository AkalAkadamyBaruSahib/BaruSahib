SET NOCOUNT ON;  

DECLARE @AcaID int , @ZoneID int;  

DECLARE curWorkallot CURSOR FOR   
SELECT AcaID,ZoneId FROM Academy  WHERE Active=1

OPEN curWorkallot



FETCH NEXT FROM curWorkallot   
INTO @AcaID,@ZoneID

WHILE @@FETCH_STATUS = 0  
BEGIN  


      insert into WorkAllot values(@ZoneID,@AcaID,'GROUND LEVELING/CLEANING',1,getdate(),3,getdate(),3,'',null,null)
        FETCH NEXT FROM curWorkallot INTO @AcaID,@ZoneID  


END   
CLOSE curWorkallot;  
DEALLOCATE curWorkallot;


