update RoomNumbers set NumOfBed=1 where BuildingID=1 and Number in (select * from dbo.[Split]('7',','))

update RoomNumbers set NumOfBed=1 where BuildingID=2 and Number in (select * from dbo.[Split]('77,91',','))

update  RoomNumbers set Number='202-B' WHERE Number='202-A-B' and BuildingID=1
update  RoomNumbers set Number='302-VIP' WHERE Number='302-A-B' and BuildingID=1
update  RoomNumbers set Number='502-B' WHERE Number='502-A-B' and BuildingID=1

UPDATE RoomNumbers SET NumOfBed=2 WHERE BuildingID=1 and Number in (SELECT * FROM dbo.[Split]('101-B,102-B,103-B,104-B,105-B,106- B,107-B,112-B,113-B,114-B,115-B,116-B,117-B,118-B,119-B,120-B,121-B,123-B,124-B,125-B,126-B,127-B,101-VIP,102-VIP,103-VIP,104-VIP,105- VIP,106-VIP,107-VIP,108-VIP,109-VIP,111-VIP,114-VIP,115-VIP,116-VIP,117-VIP,202-B,204-VIP,205-VIP,206-VIP,207-VIP,208-VIP,210-VIP,211-VIP,302- VIP,304-VIP,305-VIP,306-VIP,307-VIP,308-VIP,209-VIP,310-VIP,404-VIP,405-VIP,406-VIP,407-VIP,408-VIP,409-VIP,410-VIP,502-B,504-VIP,508-VIP,509- VIP,511-VIP,',','))


Insert into RoomNumbers values(1,'202-A',2,1)

update  RoomNumbers set Number='401-A' where  Number='401-A-B' and BuildingID=1

Insert into RoomNumbers values(1,'502-A',5,1)


UPDATE RoomNumbers SET NumOfBed=1 WHERE BuildingID=1 and Number in (SELECT * FROM dbo.[Split]('109-B,110-B,111-B,110-VIP,202-A,301- VIP,303-VIP,401-A,502-A',','))

insert into BuildingName values('Enquery Sangat')

?
