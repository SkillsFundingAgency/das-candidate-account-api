IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'GCSE')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'GCSE', 7)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'BTEC')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'BTEC', 6)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'T level')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'T level',5)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'AS level')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'AS level',4)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'A level')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'A level',3)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Apprenticeship')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'Apprenticeship',2)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Degree')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'Degree',1)
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Other')
BEGIN
insert into QualificationReference (Id, Name, [Order])
Values (NEWID(), 'Other',99)
END
