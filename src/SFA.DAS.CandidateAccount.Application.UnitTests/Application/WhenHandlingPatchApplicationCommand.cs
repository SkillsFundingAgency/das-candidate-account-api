using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingPatchApplicationCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Command_Is_Handled_And_Service_Called(
        ApplicationEntity applicationEntity,
        PatchApplication patch,
        [Frozen] Mock<IApplicationRepository> service,
        PatchApplicationCommandHandler handler)
    {
        //Arrange
        var update = applicationEntity;
        var patchCommand = new JsonPatchDocument<PatchApplication>();
        patchCommand.Replace(path => path.Status, patch.Status);
        patchCommand.Replace(path => path.TrainingCoursesStatus, patch.TrainingCoursesStatus);
        patchCommand.Replace(path => path.QualificationsStatus, patch.QualificationsStatus);
        patchCommand.Replace(path => path.JobsStatus, patch.JobsStatus);
        patchCommand.Replace(path => path.DisabilityConfidenceStatus, patch.DisabilityConfidenceStatus);
        patchCommand.Replace(path => path.SkillsAndStrengthStatus, patch.SkillsAndStrengthStatus);
        patchCommand.Replace(path => path.InterviewAdjustmentsStatus, patch.InterviewAdjustmentsStatus);
        patchCommand.Replace(path => path.AdditionalQuestion1Status, patch.AdditionalQuestion1Status);
        patchCommand.Replace(path => path.AdditionalQuestion2Status, patch.AdditionalQuestion2Status);
        patchCommand.Replace(path => path.InterestsStatus, patch.InterestsStatus);
        patchCommand.Replace(path => path.WorkExperienceStatus, patch.WorkExperienceStatus);
        var command = new PatchApplicationCommand
        {
            Id = applicationEntity.Id,
            CandidateId = applicationEntity.CandidateId,
            Patch = patchCommand 
        };
        update.Status = (short)patch.Status;
        update.TrainingCoursesStatus = (short)patch.TrainingCoursesStatus;
        update.QualificationsStatus = (short)patch.QualificationsStatus;
        update.JobsStatus = (short)patch.JobsStatus;
        update.DisabilityConfidenceStatus = (short)patch.DisabilityConfidenceStatus;
        update.SkillsAndStrengthStatus = (short)patch.SkillsAndStrengthStatus;
        update.InterviewAdjustmentsStatus = (short)patch.InterviewAdjustmentsStatus;
        update.AdditionalQuestion1Status = (short)patch.AdditionalQuestion1Status;
        update.AdditionalQuestion2Status = (short)patch.AdditionalQuestion2Status;
        update.InterestsStatus = (short)patch.InterestsStatus;
        update.WorkExperienceStatus = (short)patch.WorkExperienceStatus;
        service.Setup(x => x.GetById(command.Id)).ReturnsAsync(applicationEntity);
        service.Setup(x=>x.Update(update)).ReturnsAsync(update);
        
        //Act
        var actual = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        actual.Application.Should().BeEquivalentTo((Domain.Application.Application)update);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Exist_Null_Returned(
        PatchApplicationCommand command,
        [Frozen] Mock<IApplicationRepository> service,
        PatchApplicationCommandHandler handler)
    {
        //Arrange
        service.Setup(x => x.GetById(command.Id)).ReturnsAsync((ApplicationEntity)null);
        
        //Act
        var actual = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        actual.Should().BeEquivalentTo(new PatchApplicationCommandResponse());
        service.Verify(x=>x.Update(It.IsAny<ApplicationEntity>()), Times.Never);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Belong_To_The_Candidate_Validation_Error_Returned(
        ApplicationEntity entity,
        PatchApplicationCommand command,
        [Frozen] Mock<IApplicationRepository> service,
        PatchApplicationCommandHandler handler)
    {
        //Arrange
        service.Setup(x => x.GetById(command.Id)).ReturnsAsync(entity);
        
        Assert.ThrowsAsync<ValidationException>(()=> handler.Handle(command, CancellationToken.None));
        
    }
}