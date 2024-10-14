using System.Collections.Generic;

namespace App.TemplateParser
{
    public static class SpecialCharacters
    {
        public static readonly Dictionary<char, string> EscapeChars = new Dictionary<char, string>
        {
            {'r', "\r"},
            {'n', "\n"},
            {'\\', "\\"},
            {'{', "{"},
        };
    }
}