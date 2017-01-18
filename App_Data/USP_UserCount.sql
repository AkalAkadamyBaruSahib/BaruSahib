ALTER PROCEDURE [dbo].[USP_UserCount] --1
(              
@Id INT              
)              
as              
begin              
--UserName(0)              
select InName from Incharge where InchargeID=@Id              
--WorkAllot(1)              
select COUNT(1)as workAllot from WorkAllot wa inner join AcademyAssignToEmployee aae on aae.acaID=wa.AcaId where aae.EmpId = @Id
--RejectBill(2)     
select COUNT(1) as Co from SubmitBillByUser where CreatedBy=@Id          
--BillCount(3)              
select COUNT(1) as cou from SubmitBillByUser where SubmitBillByUser.FirstVarifyStatus=0  or SubmitBillByUser.SecondVarifyStatus=0 or SubmitBillByUser.PaymentStatus=0 or SubmitBillByUser.RecevingStatus=0 and CreatedBy=@id           
--Estimate(4)            
--select COUNT(*) as EstCo from Estimate where ZoneId = (select distinct ZoneId from AcademyAssignToEmployee where Active=1 and EmpId = (select InchargeId from Incharge where LoginId=@id))              
select COUNT(1) as EstCo from Estimate e
inner join AcademyAssignToEmployee aae on e.acaID=aae.AcaId
    where aae.EmpId = @Id
--Msg(5)        
declare @MsgTo as int    
set @MsgTo =@id    
select COUNT(1) as Msgco from Msg where MsgToUserType=2 and Active=1  and MsgTo=@MsgTo       
SELECT COUNT(*) as TicketCount FROM ComplaintTickets c
where c.CreatedBy = @Id AND (Status ='In Progress' OR  Status='Assigned')
end    



