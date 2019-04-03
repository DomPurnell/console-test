using Xunit;

namespace FileData.Tests
{
    public class ArgsCheckerTests
    {
        /// <summary>
        /// Common init logic on all tests.
        /// </summary>
        /// <param name="testValue">params to extract</param>
        /// <returns>instance of the args handler</returns>
        private IArgsHandler GetHandler(string testValue)
        {
            // Arrange
            var args = testValue.Trim().Split(' ');

            // Act
            return new ArgsHandler(args);
        }

        [Theory]
        [InlineData("-v -s test.txt")]
        [InlineData("--v --s anotherfilename.zip")]
        [InlineData("/v /s theisanewfile.jpeg")]
        [InlineData("--version --size test.txt")]
        [InlineData("--size --version test.txt")]
        [InlineData("test.txt --size --version ")]
        [InlineData("anotherfilename.zip --v --s ")]
        public void Test_Valid_Args_ForDualMatch_In_Mashup_Order(string value)
        {
            // Arrange + Act
            var argHandler = GetHandler(value);

            // Assert
            Assert.True(argHandler.ArgsValid);
            Assert.True(argHandler.SearchType.HasFlag(SearchTypeEnum.SIZE));
            Assert.True(argHandler.SearchType.HasFlag(SearchTypeEnum.VERSION));
            Assert.False(argHandler.SearchType.HasFlag(SearchTypeEnum.NONE));
        }

        [Theory]
        [InlineData("-s test.txt")]
        [InlineData("--s anotherfilename.zip")]
        [InlineData("/s theisanewfile.jpeg")]
        [InlineData("--size test.txt")]
        [InlineData("test.txt --s")]
        [InlineData("anotherfilename.zip --s ")]
        public void Test_Valid_Size_Only_Args_Set(string value)
        {
            // Arrange + Act
            var argHandler = GetHandler(value);

            // Assert
            Assert.True(argHandler.ArgsValid);
            Assert.True(argHandler.SearchType.HasFlag(SearchTypeEnum.SIZE));
            Assert.False(argHandler.SearchType.HasFlag(SearchTypeEnum.VERSION));
            Assert.False(argHandler.SearchType.HasFlag(SearchTypeEnum.NONE));
        }


        [Theory]
        [InlineData("-v test.txt")]
        [InlineData("--v anotherfilename.zip")]
        [InlineData("/v theisanewfile.jpeg")]
        [InlineData("--version test.txt")]
        [InlineData("test.txt --v")]
        [InlineData("anotherfilename.zip --v ")]
        public void Test_Valid_Version_Only_Args_Set(string value)
        {
            // Arrange + Act
            var argHandler = GetHandler(value);

            // Assert
            Assert.True(argHandler.ArgsValid);
            Assert.False(argHandler.SearchType.HasFlag(SearchTypeEnum.SIZE));
            Assert.True(argHandler.SearchType.HasFlag(SearchTypeEnum.VERSION));
            Assert.False(argHandler.SearchType.HasFlag(SearchTypeEnum.NONE));
        }


        [Theory]
        [InlineData("-v -j .txt")]
        [InlineData("-ver -s .txt")]
        [InlineData("--z --s anotherfilename.zip")]
        [InlineData("/v /s .jpeg")]
        [InlineData("--gze test.txt")]
        [InlineData("--tanker test.txt")]
        [InlineData("test.txt --yy --version ")]
        [InlineData("anotherfilename.zip --j --s ")]
        public void Test_For_Invalid_Args(string value)
        {
            // Arrange + Act
            var argHandler = GetHandler(value);

            // Assert
            Assert.False(argHandler.ArgsValid);
        }
    }
}
