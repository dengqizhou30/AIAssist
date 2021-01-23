using System.Reflection;

namespace LowLevelInput
{
    /// <summary>
    ///     Provides displayable information about this library.
    /// </summary>
    public static class Library
    {
        /// <summary>
        ///     Gets the author of this library.
        /// </summary>
        /// <value>
        ///     The name of the author.
        /// </value>
        public static string Author => "michel-pi";

        /// <summary>
        ///     Gets the name of this library.
        /// </summary>
        /// <value>
        ///     The name of the library.
        /// </value>
        public static string Name => "LowLevelInput.Net";

        /// <summary>
        ///     Gets the projects url.
        /// </summary>
        /// <value>
        ///     The projects url.
        /// </value>
        public static string ProjectUrl => "https://github.com/michel-pi/LowLevelInput.Net";

        /// <summary>
        ///     Gets the version of this library.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public static string Version
        {
            get
            {
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var assemblyName = assembly.GetName();

                    return assemblyName.Version.ToString();
                }
                catch
                {
                    return "1.0.0.0";
                }
            }
        }
    }
}