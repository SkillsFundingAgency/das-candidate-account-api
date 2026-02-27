using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteSavedVacancy;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.SavedVacancies
{
    [TestFixture]
    public class WhenCallingDeleteSavedVacancy
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid candidateId,
            string vacancyId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] SavedVacancyController controller)
        {
            var result = await controller.DeleteSavedVacancy(candidateId, vacancyId, false) as NoContentResult;

            result.Should().BeOfType<NoContentResult>();

            mediator.Verify(x => x.Send(It.Is<DeleteSavedVacancyCommand>(c =>
                c.CandidateId.Equals(candidateId) &&
                c.VacancyId.Equals(vacancyId)
            ), CancellationToken.None));
        }

        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Exception_Returned_As_InternalServerException(
            Guid candidateId,
            string vacancyId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] SavedVacancyController controller)
        {

            mediator.Setup(x => x.Send(It.Is<GetSavedVacancyQuery>(c => c.CandidateId == candidateId && c.VacancyId == vacancyId), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var actual = await controller.GetByVacancyReference(candidateId, vacancyId, null);

            actual.Should().BeOfType<StatusCodeResult>();
            var result = actual as StatusCodeResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}