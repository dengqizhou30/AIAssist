using LowLevelInput.Hooks;

namespace LowLevelInput.Converters
{
    /// <summary>
    ///     Provides converter methods for KeyStateConverter
    /// </summary>
    public static class KeyStateConverter
    {
        private static readonly string[] KeyStateMap =
        {
            "None",
            "Up",
            "Down",
            "Pressed"
        };

        /// <summary>
        ///     Converts a <c>string</c> to it's corresponding <c>KeyState</c>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static KeyState ToKeyState(string name)
        {
            if (string.IsNullOrEmpty(name)) return KeyState.None;
            if (string.IsNullOrWhiteSpace(name)) return KeyState.None;

            string tmp = name.ToLower();

            for (int i = 0; i < KeyStateMap.Length; i++)
                if (tmp == KeyStateMap[i].ToLower()) return (KeyState) i;

            return KeyState.None;
        }

        /// <summary>
        ///     Converts an <c>int</c> to it's corresponding <c>KeyState</c>
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static KeyState ToKeyState(int state)
        {
            if (state < 0) return KeyState.None;
            if (state >= KeyStateMap.Length) return KeyState.None;

            return (KeyState) state;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents a <c>KeyState</c>.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A <see cref="System.String" /> that represents a <c>KeyState</c>.</returns>
        public static string ToString(KeyState state)
        {
            int index = (int) state;

            if (index < 0) return "None";

            return index >= KeyStateMap.Length ? "None" : KeyStateMap[index];
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(int index)
        {
            if (index < 0) return "None";

            return index >= KeyStateMap.Length ? "None" : KeyStateMap[index];
        }
    }
}