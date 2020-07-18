using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public static class CustomNumberValidator
    {
        public static string NumberValidation(string number)
        {
            var rgx = new Regex(@"^\d{4}$");
            if (rgx.IsMatch(number))
                return null;
            return "Numer może zawierać tylko liczby 0-9.";
        }
    }
}
