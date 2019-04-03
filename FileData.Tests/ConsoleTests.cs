using FileInfoAPI;
using System.IO;
using Xunit;

namespace FileData.Tests
{
    // Really a form of blackbox testing for the class.
    public class ConsoleTests
    {
        private string GetConsoleOutput(string testValues)
        {
            // Arrange
            var consoleOutStream = new StringWriter();
            var fileInfoService = new FileInfoService();

            var args = testValues.Trim().Split(' ');
            var argsHandler = new ArgsHandler(args);

            // Act
            Program.RunAsTest(consoleOutStream, fileInfoService, argsHandler);

            return consoleOutStream.ToString();
        }

        [Theory]
        [InlineData("-s test.txt")]
        [InlineData("--s anotherfilename.zip")]
        [InlineData("/s theisanewfile.jpeg")]
        [InlineData("--size test.txt")]
        [InlineData("test.txt --s")]
        [InlineData("anotherfilename.zip --s ")]
        public void Check_That_FileSize_Outputted(string value)
        {
            // Arrange +  Act
            var consoleOutputText = GetConsoleOutput(value);

            // Assert
            Assert.Contains("File size :", consoleOutputText);
        }

        [Theory]
        [InlineData("-v -s test.txt")]
        [InlineData("--v --s anotherfilename.zip")]
        [InlineData("/v /s theisanewfile.jpeg")]
        [InlineData("--version --size test.txt")]
        [InlineData("--size --version test.txt")]
        [InlineData("test.txt --size --version ")]
        [InlineData("anotherfilename.zip --v --s ")]
        public void Check_That_FileSizeAndVersion_Outputted(string value)
        {
            // Arrange +  Act
            var consoleOutputText = GetConsoleOutput(value);

            // Assert
            Assert.Contains("File version :", consoleOutputText);
            Assert.Contains("File size :", consoleOutputText);
        }

        [Theory]
        [InlineData("-v test.txt")]
        [InlineData("--v anotherfilename.zip")]
        [InlineData("/v theisanewfile.jpeg")]
        [InlineData("--version test.txt")]
        [InlineData("test.txt --v")]
        [InlineData("anotherfilename.zip --v ")]
        public void Check_That_FileVersion_Outputted(string value)
        {
            // Arrange +  Act
            var consoleOutputText = GetConsoleOutput(value);

            // Assert
            Assert.Contains("File version :", consoleOutputText);
        }

        //TestBadArgs.
        [Theory]
        [InlineData("-sdfasdf test.txt")]
        [InlineData("--p")]
        [InlineData("/v")]
        [InlineData("--hhn test.txt")]
        [InlineData("test.asdfasdfasdf --asdfv")]
        [InlineData("")]
        public void Check_If_Output_OK_With_Bad_Args(string value)
        {
            // Arrange +  Act
            var consoleOutputText = GetConsoleOutput(value);

            // Assert
            Assert.Contains("FileData usage as follows", consoleOutputText);
        }
    }
}
