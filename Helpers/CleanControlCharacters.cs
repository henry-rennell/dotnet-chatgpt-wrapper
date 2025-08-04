using System.Text;

//Provides static methods for sanitizing string inputs by replacing control characters
namespace FunctionApp.Helpers
{
    public static class CleanControlCharacters
    {
        public static string ReplaceControlCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var sb = new StringBuilder(input.Length);

            foreach (var c in input)
            {
                switch (c)
                {
                    case '\n':
                        sb.Append("<<NEWLINE>>");
                        break;
                    case '\r':
                        sb.Append("<<CARRIAGE_RETURN>>");
                        break;
                    case '\t':
                        sb.Append("<<TAB>>");
                        break;
                    default:
                        if (char.IsControl(c))
                        {
                            // Remove all other control chars.
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
