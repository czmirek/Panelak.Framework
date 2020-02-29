namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions over ModelStateDictionary
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Merges collection of strings into the model state dictionary as errors
        /// </summary>
        /// <param name="modelStateDictionary">Model state dictionary</param>
        /// <param name="errorsDictionary">Errors dictionary</param>
        public static void MergeErrors(this ModelStateDictionary modelStateDictionary, IEnumerable<KeyValuePair<string, string>> errorsDictionary)
        {
            foreach (KeyValuePair<string, string> item in errorsDictionary)
                modelStateDictionary.AddModelError(item.Key, item.Value);
        }
    }
}
