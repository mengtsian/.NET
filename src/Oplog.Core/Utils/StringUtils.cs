namespace Oplog.Core.Utils;

public static class StringUtils
{
    public static string ConvertFirstLetterToUpper(string input)
    {
        if (input == null)
        {
            return null;
        }

        return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
    }
}
