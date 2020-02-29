using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface ITagStore
    {
        void Create(string tagName);
        void TagQuestion(int questionId, int tagId);
        IEnumerable<NewTag> ListAvailableTags(int questionId);
        bool Exists(string tagName);
        bool Exists(int tagId);
        bool IsTagged(int questionId, int tagId);
    }
}