namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;

public class GetApplicationQueryResult
{
    public Domain.Application.Application? Application { get; set; }
    public List<Domain.Application.Question> AdditionalQuestions { get; set; } = [];
}