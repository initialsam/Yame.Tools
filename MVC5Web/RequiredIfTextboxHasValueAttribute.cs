using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC5Web
{
    public class RequiredIfTextboxHasValueAttribute : ValidationAttribute, IClientValidatable
    {
        private String PropertyName { get; set; }


        public RequiredIfTextboxHasValueAttribute(String propertyName)
        {
            PropertyName = propertyName;

        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var dependentValue = context.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(context.ObjectInstance, null);

            if (dependentValue != null && String.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult($"{context.DisplayName}是必填欄位", new[] { context.MemberName });
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = string.Format("{0}是必填欄位", metadata.GetDisplayName()),
                ValidationType = "requirediftextboxhasvalue",
            };
            rule.ValidationParameters["dependentproperty"] = PropertyName; ;

            yield return rule;
        }
    }
}
