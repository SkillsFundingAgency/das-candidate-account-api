
--add default qualification references

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

IF NOT EXISTS(SELECT 1 FROM Preference where PreferenceMeaning = 'Your application gets a response')
BEGIN
insert into Preference (PreferenceId, PreferenceMeaning, PreferenceHint)
Values (NEWID(), 'Your application gets a response', 'Be notified when an application is successful or unsuccessful')
END

--add default notification preferences

IF NOT EXISTS(SELECT 1 FROM Preference where PreferenceMeaning = 'A vacancy is closing soon')
BEGIN
insert into Preference (PreferenceId, PreferenceMeaning, PreferenceHint)
Values (NEWID(), 'A vacancy is closing soon', 'Get a reminder 7 days before the closing date for an apprenticeship. We’ll notify you for apprenticeships you’ve began an application for.')
END