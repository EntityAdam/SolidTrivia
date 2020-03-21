using System;
using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface ITagStore
    {
        void Create(string tagName);
        IEnumerable<NewTag> ListTags();
        void TagQuestion(Guid questionId, Guid tagId);
        IEnumerable<NewTag> ListAvailableTags(Guid questionId);
        bool ExistsOrdinalIgnoreCase(string tagName);
        bool Exists(Guid tagId);
        bool IsTagged(Guid questionId, Guid tagId);
        void Delete(Guid tagId);
        NewTag GetById(Guid tagId);
        void Rename(Guid tagId, string newTagName);
    }
}