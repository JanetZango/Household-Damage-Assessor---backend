using System.ComponentModel;

namespace App.TemplateParser
{
    public enum Placeholder
    {
        [Description(@"\{([a-z0-9_.\-]+)\}")]
        Brace,
        [Description(@"\[([a-z0-9_.\-]+)\]")]
        Bracket
    }
}