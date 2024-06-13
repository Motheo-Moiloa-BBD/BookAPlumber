using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookAPlumber.Core.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(errors => errors.Value.Errors)
                             .Select(error => error.ErrorMessage)
                             .ToList();
        }
    }
}
