using System;
using System.Globalization;
using System.Windows.Controls;

namespace FFmpeg.Validations
{
    class FieldValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int.Parse(value.ToString());
            } catch (FormatException)
            {
                return new ValidationResult(true, "Number only!");
            }
            return new ValidationResult(true, null);
        }
    }
}
