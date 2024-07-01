namespace RSpeditionWEB.Validation
{
    public static class ValidateModel
    {
        public static (bool isValid, List<string> errors) CheckPropertyModelIsValid(this object model, string prop)
        {
            if (model == null || model == default)
                return (false, new List<string> { "Модель пустая" });

            var isModelValid = true;
            List<string> errorsMessages = new();

            var validResults = new List<ValidationResult>();
            var context = new ValidationContext(model);
            isModelValid = Validator.TryValidateObject(model, context, validResults, true);

            if (!isModelValid)
            {
                foreach (var error in validResults)
                {
                    if (error.MemberNames.Any(_ => _.Equals(prop)))
                        errorsMessages.Add(error.ErrorMessage);
                }

                if (errorsMessages?.Count > 0)
                    isModelValid = false;
            }
            else
            {
                var editContext = new Microsoft.AspNetCore.Components.Forms.EditContext(model);
                errorsMessages = editContext?.GetValidationMessages()?.ToList() ?? new();

                if (errorsMessages?.Count > 0)
                    isModelValid = false;
            }

            return (isModelValid, errorsMessages);
        }

        public static string GetValidationMessageForProp(this object model, string prop)
            => model.CheckPropertyModelIsValid(prop).errors.ConvertListToString();

        public static (bool isValid, List<string> errors) CheckModelIsValid(this object model)
        {
            if (model == null || model == default)
                return (false, new List<string> { "Модель пустая" });

            var isModelValid = true;
            List<string> errorsMessages = new();

            var validResults = new List<ValidationResult>();

            var context = new ValidationContext(model);

            var modelIsValid = Validator.TryValidateObject(model, context, validResults, true);

            if (!modelIsValid)
            {
                foreach (var error in validResults)
                {
                    errorsMessages.Add(error.ErrorMessage);
                }

                isModelValid = false;
            }
            else
            {
                var editContext = new Microsoft.AspNetCore.Components.Forms.EditContext(model);
                errorsMessages = editContext?.GetValidationMessages().ToList() ?? new();
            }

            return (isModelValid, errorsMessages);
        }
    }
}
