using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class TagStoreDummy : ITagStore
    {
        public void Create(string tagName)
        {
            throw new NotImplementedException();
        }

        public bool ExistsOrdinalIgnoreCase(string tagName)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public bool IsTagged(Guid questionId, Guid tagId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTag> ListAvailableTags(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTag> ListTags()
        {
            throw new NotImplementedException();
        }

        public void TagQuestion(Guid questionId, Guid tagId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public NewTag GetById(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public void Rename(Guid tagId, string newTagName)
        {
            throw new NotImplementedException();
        }
    }
}
