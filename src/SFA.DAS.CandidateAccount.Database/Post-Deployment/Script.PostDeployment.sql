
--add default qualification references

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

--add default notification preferences

IF NOT EXISTS(SELECT 1 FROM Preference where PreferenceMeaning = 'A vacancy is closing soon')
BEGIN
insert into Preference (PreferenceId, PreferenceMeaning, PreferenceHint, PreferenceType)
Values (NEWID(), 'A vacancy is closing soon', 'Get a reminder 7 days before the closing date for an apprenticeship. We''ll notify you for your saved vacancies and apprenticeships you''ve began an application for.','closing')
END
ELSE
BEGIN
    UPDATE Preference set PreferenceHint = 'Get a reminder 7 days before the closing date for an apprenticeship. We''ll notify you for your saved vacancies and apprenticeships you''ve began an application for.', PreferenceType='closing' where PreferenceMeaning = 'A vacancy is closing soon'
END