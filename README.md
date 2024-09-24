
## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# das-candidate-account-api

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-candidate-account-api?repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=refs%2Fpull%2F72%2Fmerge)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3561&repoName=SkillsFundingAgency%2Fdas-candidate-account-api&branchName=refs%2Fpull%2F72%2Fmerge)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-candidate-account-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=SkillsFundingAgency_das-candidate-account-api)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

This Candidate Account Api solution is part of the FindAnApprenticeship project. This represents the inner API for candidate and application data for the [Find an apprenticeship service](https://www.gov.uk/apply-apprenticeship).

## Installation

### Pre-Requisites

* A clone of this repository
* A code editor that supports Azure functions and .NetCore 8.0
* An Azure Active Directory account with the appropriate roles as per the [config](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-candidate-account-api/SFA.DAS.CandidateAccount.Api.json)
* SQL server - Publish the `SFA.DAS.CandidateAccount.Database` project to create the SQL database
* Azure Storage Emulator(https://learn.microsoft.com/en-us/azure/storage/common/storage-use-emulator)

### Local running

The Candidate Account api uses the standard Apprenticeship Service configuration. All configuration can be found in the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-candidate-account-api/SFA.DAS.CandidateAccount.Api.json).

* appsettings.json file
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
  "ConfigNames": "SFA.DAS.CandidateAccount.Api",
  "EnvironmentName": "LOCAL",
  "Version": "1.0"
}
```

You must have the Azure Storage emulator running, and in that a table created called `Configuration` in that table add the following:

Azure Table Storage config

Row Key: SFA.DAS.CandidateAccount.Api_1.0

Partition Key: LOCAL

Data:

```json
{
  "CandidateAccountConfiguration": {
    "ConnectionString": "Data Source=.;Initial Catalog=SFA.DAS.CandidateAccount;User Id=sa;Password={YOUR_PASSWORD};Pooling=False;Connect Timeout=30;Encrypt=False;"
  },
  "AzureAd": {
    "tenant": "***.onmicrosoft.com",
    "identifier": "https://*****.onmicrosoft.com/"
  }
}
```

## Technologies

* .NetCore 8.0
* SQL
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions

## How It Works

### Running

* Open command prompt and change directory to _**/src/SFA.DAS.CandidateAccount.Api/**_
* Run the web project _**/src/SFA.DAS.CandidateAccount.Api/SFA.DAS.CandidateAccount.Api.csproj**_

MacOS
```
ASPNETCORE_ENVIRONMENT=Development dotnet run
```
Windows cmd
```
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

### Application logs
Application logs are logged to [Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) and can be viewed using [Azure Monitor](https://learn.microsoft.com/en-us/azure/azure-monitor/overview) at https://portal.azure.com

## Useful URLs

### Candidates

https://localhost:7277/api/Candidate/{candidateId} - Endpoint to get candidate information by given candidate id.

https://localhost:7277/api/Candidate/{candidateId}/about-you - Endpoint to get about you information by given candidate id.

https://localhost:7277/api/Candidate/{candidateId}/address - Endpoint to get address information by given candidate id.


### Application

https://localhost:7277/api/Candidates/{candidateId}/applications - Endpoint to get all applications

https://localhost:7277/api/Candidates/{candidateId}/applications/{applicationId} - Endpoint to get application information by given application id.

https://localhost:7277/api/Candidates/{candidateId}/applications/GetByReference/{vacancyReference} - Endpoint to get application by given vacancy reference.

## License

Licensed under the [MIT license](LICENSE)