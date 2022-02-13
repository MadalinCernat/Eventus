using System.ComponentModel.DataAnnotations;

namespace EventusUI.ValidationAttributes
{
    public class FromNowAttribute : ValidationAttribute
    {
        private string _errorMessage = "Date must be past now.";
        public FromNowAttribute()
        {

        }
        public FromNowAttribute(string ErrorMessage)
        {
            _errorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) < 0) return new ValidationResult(_errorMessage);
            else return ValidationResult.Success;
        }
    }
}
