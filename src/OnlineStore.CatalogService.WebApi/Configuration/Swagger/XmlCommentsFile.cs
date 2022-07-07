using System.Reflection;

namespace OnlineStore.CatalogService.WebApi.Configuration.Swagger
{
    /// <summary>
    /// XML comments file.
    /// </summary>
    public class XmlCommentsFile
    {
        /// <summary>
        /// Gets directory name.
        /// </summary>
        /// <value>
        /// <placeholder>Directory name.</placeholder>
        /// </value>
        public static string Directory => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;

        /// <summary>
        /// Gets XML documentation file name.
        /// </summary>
        /// <value>
        /// <placeholder>XML documentation file name.</placeholder>
        /// </value>
        public static string FileName => typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";

        /// <summary>
        /// Gets full file path.
        /// </summary>
        /// <value>
        /// <placeholder>Full file path.</placeholder>
        /// </value>
        public static string FullFilePath => Path.Combine(Directory, FileName);
    }
}
