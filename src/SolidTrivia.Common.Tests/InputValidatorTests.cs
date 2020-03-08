using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolidTrivia.Common.Tests
{
    public class InputValidatorTests
    {
        [Fact]
        public void Test()
        {
           Assert.True(InputValidator.IsValidTagName("aaa"));
           Assert.True(InputValidator.IsValidCategory("category"));
        }

    }
}
