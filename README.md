
## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# das-candidate-account-api

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status]([https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-candidate-account-api?repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=main)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3561&repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=main](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-candidate-account-api?repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=main)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3561&repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=main))
[![Quality Gate Status]([https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-candidate-account-api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkillsFundingAgency_das-candidate-account-api](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-candidate-account-api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkillsFundingAgency_das-candidate-account-api))

## Requirements


-   .net6 and any supported IDE for DEV running.
-   _If you are not wishing to run the in memory database then_
-   SQL Server database.
-   Azure Storage Account

## About
This represents the inner API for candidate and application data for the [Find an apprenticeship service](https://www.gov.uk/apply-apprenticeship).

## Local running
You are able to run the API by doing the following:

-   Run the database deployment publish command to create the database  `SFA.DAS.CandidateAccount.Database`  or create the database manually and run in the table creation scripts
-   In your Azure Storage Account, create a table called Configuration and Add the following
```
ParitionKey: LOCAL
RowKey: SFA.DAS.CandidateAccount.Api_1.0
Data: {"CandidateAccountConfiguration": { "ConnectionString": "Data Source=.;Initial Catalog=SFA.DAS.CandidateAccount;User Id=sa;Password={YOUR_PASSWORD};Pooling=False;Connect Timeout=30;Encrypt=False;" },"AzureAd":{"tenant":"***.onmicrosoft.com","identifier":"https://***.onmicrosoft.com/****"}}
```
- start the `SFA.DAS.CandidateAccount.Api`