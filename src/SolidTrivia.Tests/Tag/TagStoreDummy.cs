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

        public bool Exists(int tagId)
        {
            throw new NotImplementedException();
        }

        public bool IsTagged(int questionId, int tagId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTag> ListAvailableTags(int questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTag> ListTags()
        {
            throw new NotImplementedException();
        }

        public void TagQuestion(int questionId, int tagId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int tagId)
        {
            throw new NotImplementedException();
        }

        public NewTag GetById(int tagId)
        {
            throw new NotImplementedException();
        }

        public void Rename(int tagId, string newTagName)
        {
            throw new NotImplementedException();
        }
    }
}
