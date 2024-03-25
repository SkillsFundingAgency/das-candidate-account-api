IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'GCSE')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'GCSE')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'BTEC')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'BTEC')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'T level')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'T level')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'AS level')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'AS level')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'A level')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'A level')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Apprenticeship')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'Apprenticeship')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Degree')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'Degree')
END
IF NOT EXISTS(SELECT 1 FROM QualificationReference where Name = 'Other')
BEGIN
insert into QualificationReference (Id, Name)
Values (NEWID(), 'Other')
END