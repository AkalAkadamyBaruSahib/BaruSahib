ALTER TABLE MaterialRateApproved ADD EstID int NULL

ALTER TABLE MaterialNonApprovedRate ADD EstID int NULL

Update MaterialRateApproved Set EstID=0

Update MaterialNonApprovedRate Set EstID=0