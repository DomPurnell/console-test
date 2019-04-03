using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FileData
{
    /// <summary>
    /// This class validates any user input with the maximum of flexibility.
    /// </summary>
    public class ArgsHandler : IArgsHandler
    {
        private const string _validFileNameRegex = @"^[^\.].*\..{1,6}$";
        private readonly List<string> _validVersionParams = new List<string>() { "-v", "--v", "/v", "--version" };
        private readonly List<string> _validSizeParams = new List<string>() { "-s", "--s", "/s", "--size" };

        public string Filename { get; }

        public bool ArgsValid { get; }

        public SearchTypeEnum SearchType { get; }

        /// <summary>
        /// Constructor will initialise the class and populate i/f members.
        /// </summary>
        /// <param name="args"></param>
        public ArgsHandler(string[] args)
        {

            if (args == null || args.Length < 2 || args.Length > 3)
                ArgsValid = false;
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    var a = args[i].Trim().ToLower();

                    if (Regex.IsMatch(a, _validFileNameRegex))
                        Filename = a;
                    else if (_validVersionParams.Contains(a))
                        SearchType = SearchType == SearchTypeEnum.NONE ? SearchTypeEnum.VERSION : SearchType | SearchTypeEnum.VERSION;
                    else if (_validSizeParams.Contains(a))
                        SearchType = SearchType == SearchTypeEnum.NONE ? SearchTypeEnum.SIZE : SearchType | SearchTypeEnum.SIZE;
                    else // Invalid arg
                    {
                        SearchType = SearchTypeEnum.NONE;
                        break;
                    }
                }

                ArgsValid = Filename != string.Empty && SearchType != SearchTypeEnum.NONE;
            }
        }
    }
}
