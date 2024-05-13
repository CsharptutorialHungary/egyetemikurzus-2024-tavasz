namespace WHBNDL.Domain
{
    public sealed record class Question
    {
        public required string QuestionText { get; set; }
        public required Answer Answers { get; set; }
    }
}
