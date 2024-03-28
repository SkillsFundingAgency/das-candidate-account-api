IF NOT EXISTS(SELECT 1 FROM Preference where PreferenceMeaning = 'Your application gets a response')
BEGIN
insert into Preference (PreferenceId, PreferenceMeaning, PreferenceHint)
Values (NEWID(), 'Your application gets a response', 'Be notified when an application is successful or unsuccessful')
END

IF NOT EXISTS(SELECT 1 FROM Preference where PreferenceMeaning = 'A vacancy is closing soon')
BEGIN
insert into Preference (PreferenceId, PreferenceMeaning, PreferenceHint)
Values (NEWID(), 'A vacancy is closing soon', 'Get a reminder 7 days before the closing date of an apprenticeship. We''ll notify you for apprenticeships you''ve began an application for.')
END