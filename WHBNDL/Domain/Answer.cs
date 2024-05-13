namespace WHBNDL.Domain
{
    public sealed record class Answer
    {
        public required string CorrectAnswer { get; set; }
        public required string[] WrongAnswers { get; set; }
    }
}
