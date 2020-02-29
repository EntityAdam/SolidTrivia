namespace SolidTrivia.Questions
{
    public class NewTag
    {
        public int Id { get; set; }

        public int? TagId { get; set; }

        public int? QuestionId { get; set; }
        
        public string Name { get; set; }
    }
}