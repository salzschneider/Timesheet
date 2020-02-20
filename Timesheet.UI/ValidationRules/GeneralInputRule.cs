using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Timesheet.UI.ValidationRules
{
    public class GeneralInputRule : ValidationRule
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int valueLength = value.ToString().Length;

            if (valueLength < MinLength || valueLength > MaxLength)
            {
                return new ValidationResult(false, $"Please enter a valid value in the length range: {MinLength}-{MaxLength}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
