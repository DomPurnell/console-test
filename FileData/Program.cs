using FileInfoAPI;
using System;
using System.IO;

namespace FileData
{
    // DI tricky with a static class.
    public static class Program
    {
        private static IFileInfoService _fileinfoService = null;
        private static IArgsHandler _argsHandler = null;

        /// <summary>
        /// Entry point for console.
        /// </summary>
        /// <param name="args">command line args</param>
        public static void Main(string[] args)
        {
            var isTest = _argsHandler != null;

            _fileinfoService = _fileinfoService ?? new FileInfoService();
            _argsHandler = _argsHandler ?? new ArgsHandler(args);

            if (!_argsHandler.ArgsValid)
                WriteUsageInstructions();
            else
            {
                var info = _fileinfoService.GetFileInfo(_argsHandler.Filename);

                Console.WriteLine(string.Format("Information requested for : {0}", _argsHandler.Filename));

                if (_argsHandler.SearchType.HasFlag(SearchTypeEnum.SIZE))
                {
                    Console.WriteLine(string.Format("File size : {0}", info.Size));
                }

                if (_argsHandler.SearchType.HasFlag(SearchTypeEnum.VERSION))
                {
                    Console.WriteLine(string.Format("File version : {0}", info.Version));
                }
            }

            Console.WriteLine("Press any key to finish.");

            // One gotcha at the end of test. Blocks stream output in test class.
            if (!isTest)
                Console.ReadKey();
        }

        /// <summary>
        /// User help message.
        /// </summary>
        private static void WriteUsageInstructions()
        {
            Console.WriteLine("###########################################################");
            Console.WriteLine(" FileData usage as follows...");
            Console.WriteLine(" filedata.exe {version} {size} {filename} *{} in any order");
            Console.WriteLine(" e.g. filedate.exe -v -s c:\test.txt");
            Console.WriteLine(" {version flag} as either -v, --v, /v, --version");
            Console.WriteLine(" {size flag} as either -s, --s, /s, --size");
            Console.WriteLine("###########################################################");
        }


        /// <summary>
        /// Challenge being as a static class I can't inject dependency’s on a constructor therefore
        /// using overload of the main function so I can inject from my test class any Moq's etc.
        /// This then makes gives the class 100% code coverage in test.
        /// </summary>
        /// <param name="args">command line args to test</param>
        /// <param name="writer">Stream to output to</param>
        /// <param name="fileinfoService">Moq for info service</param>
        /// <param name="argsHandler">Moq for args handler</param>
        public static void RunAsTest(TextWriter writer, IFileInfoService fileinfoService, IArgsHandler argsHandler)
        {
            _fileinfoService = fileinfoService;
            _argsHandler = argsHandler;
            Console.SetOut(writer);
            Program.Main(null);
        }

    }
}
