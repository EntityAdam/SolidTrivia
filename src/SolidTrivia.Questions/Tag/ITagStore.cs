using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface ITagStore
    {
        void Create(string tagName);
        IEnumerable<NewTag> ListTags();
        void TagQuestion(int questionId, int tagId);
        IEnumerable<NewTag> ListAvailableTags(int questionId);
        bool ExistsOrdinalIgnoreCase(string tagName);
        bool Exists(int tagId);
        bool IsTagged(int questionId, int tagId);
        void Delete(int tagId);
    }
}