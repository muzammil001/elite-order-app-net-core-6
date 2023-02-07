using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EliteOrderApp.Web.Extensions
{
    static class ModelStateExtension
    {
        public static string GetFullErrorMessage(this ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                messages.AddRange(entry.Value.Errors.Select(error => error.ErrorMessage));
            }

            return string.Join(" ", messages);
        }
    }
}
