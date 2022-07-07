using FluentValidation;

namespace OnlineStore.CatalogService.Application.Common.Extensions
{
    /// <summary>
    /// Rule builder extensions.
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Defines a valid URL.
        /// </summary>
        /// <typeparam name="T">Type of object for validation.</typeparam>
        /// <param name="ruleBuilder">Rule builder.</param>
        /// <returns>Rule builder options.</returns>
        public static IRuleBuilderOptions<T, string> Url<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            bool UrlIsValidUri(string url) => Uri.TryCreate(url, UriKind.Absolute, out var outUri)
               && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);

            return ruleBuilder.Must(UrlIsValidUri);
        }
    }
}
