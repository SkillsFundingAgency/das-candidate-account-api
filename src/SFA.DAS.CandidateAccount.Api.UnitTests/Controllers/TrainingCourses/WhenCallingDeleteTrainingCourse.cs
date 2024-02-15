using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteTrainingCourse;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.TrainingCourses
{
    public class WhenCallingDeleteTrainingCourse
    {
        [Test, MoqAutoData]
        public async Task Then_If_MediatorCall_Returns_Ok_Then_Ok_Result_Returned(
          Guid candidateId,
          Guid applicationId,
          Guid id,
          [Frozen] Mock<IMediator> mediator,
          [Greedy] TrainingCoursesController controller)
        {
            var actual = await controller.DeleteTrainingCourse(candidateId, applicationId, id) as OkObjectResult;

            actual.Should().BeOfType<OkObjectResult>();
            mediator.Verify(x => x.Send(It.Is<DeleteTrainingCourseCommand>(c =>
                    c.CandidateId.Equals(candidateId) &&
                    c.ApplicationId.Equals(applicationId) &&
                    c.JobId.Equals(id)
                ), CancellationToken.None));

        }

        [Test, MoqAutoData]
        public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
            Guid candidateId,
            Guid applicationId,
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] TrainingCoursesController controller)
        {
            // Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteTrainingCourseCommand>(), CancellationToken.None))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var actual = await controller.DeleteTrainingCourse(candidateId, applicationId, id);

            // Assert
            actual.Should().BeOfType<StatusCodeResult>();
            var result = actual as StatusCodeResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

    }
}
