using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class TagStoreMock : ITagStore
    {
        public List<NewTag> Tags { get; set; } = new List<NewTag>();
        public List<NewTaggedQuestion> TaggedQuestions { get; set; } = new List<NewTaggedQuestion>();

        public TagStoreMock()
        {

        }

        public void Create(string tagName) => Tags.Add(new NewTag() { Id = NewId(), Name = tagName });

        public IEnumerable<NewTag> ListTags() => Tags;

        public void TagQuestion(int questionId, int tagId) => TaggedQuestions.Add(new NewTaggedQuestion() { QuestionId = questionId, TagId = tagId });

        public bool ExistsOrdinalIgnoreCase(string tagName) => Tags.Any(t => string.Equals(t.Name, tagName, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<NewTag> ListAvailableTags(int questionId) => Tags.Where(t => !TaggedQuestions.Any(tq => tq.QuestionId == questionId && tq.TagId == t.Id));

        private int NewId()
        {
            var id = 1;
            if (Tags.Count() > 0)
            {
                id = Tags.Max(c => c.Id) + 1;
            }
            return id;
        }

        public bool Exists(int tagId) => Tags.Any(t => t.Id == tagId);

        public bool IsTagged(int questionId, int tagId) => TaggedQuestions.Any(tq => tq.QuestionId == questionId && tq.TagId == tagId);

        public void Delete(int tagId)
        {
            var questions = TaggedQuestions.RemoveAll(tq=>tq.TagId == tagId); //remove tags from questions
            Tags.RemoveAll(t=>t.Id == tagId);
        }

        public NewTag GetById(int tagId) => Tags.Single(t=>t.Id == tagId);

        public void Rename(int tagId, string newTagName) => GetById(tagId).Name = newTagName;
    }
}
