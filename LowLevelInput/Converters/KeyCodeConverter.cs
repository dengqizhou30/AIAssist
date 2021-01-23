using System.Collections.Generic;

using LowLevelInput.Hooks;

namespace LowLevelInput.Converters
{
    /// <summary>
    ///     Provides converter methods for VirtualKeyCodes
    /// </summary>
    public static class KeyCodeConverter
    {
        private static Dictionary<string, int> keyCodeToIndex = new Dictionary<string, int>();
        private static Dictionary<int, string> indexToKeyCode = new Dictionary<int, string>();

        static KeyCodeConverter()
        {
            #region fill keyCodeToIndex
            keyCodeToIndex.Add("Hotkey", 0x0);
            keyCodeToIndex.Add("Lbutton", 0x1);
            keyCodeToIndex.Add("Rbutton", 0x2);
            keyCodeToIndex.Add("Cancel", 0x3);
            keyCodeToIndex.Add("Mbutton", 0x4);
            keyCodeToIndex.Add("Xbutton1", 0x5);
            keyCodeToIndex.Add("Xbutton2", 0x6);
            keyCodeToIndex.Add("Back", 0x8);
            keyCodeToIndex.Add("Tab", 0x9);
            keyCodeToIndex.Add("Clear", 0xC);
            keyCodeToIndex.Add("Return", 0xD);
            keyCodeToIndex.Add("Shift", 0x10);
            keyCodeToIndex.Add("Control", 0x11);
            keyCodeToIndex.Add("Menu", 0x12);
            keyCodeToIndex.Add("Pause", 0x13);
            keyCodeToIndex.Add("Capital", 0x14);
            keyCodeToIndex.Add("Kana", 0x15);
            keyCodeToIndex.Add("Hangul", 0x16);
            keyCodeToIndex.Add("Junja", 0x17);
            keyCodeToIndex.Add("Final", 0x18);
            keyCodeToIndex.Add("Hanja", 0x19);
            keyCodeToIndex.Add("Kanji", 0x1A);
            keyCodeToIndex.Add("Escape", 0x1B);
            keyCodeToIndex.Add("Convert", 0x1C);
            keyCodeToIndex.Add("Nonconvert", 0x1D);
            keyCodeToIndex.Add("Accept", 0x1E);
            keyCodeToIndex.Add("Modechange", 0x1F);
            keyCodeToIndex.Add("Space", 0x20);
            keyCodeToIndex.Add("Prior", 0x21);
            keyCodeToIndex.Add("Next", 0x22);
            keyCodeToIndex.Add("End", 0x23);
            keyCodeToIndex.Add("Home", 0x24);
            keyCodeToIndex.Add("Left", 0x25);
            keyCodeToIndex.Add("Up", 0x26);
            keyCodeToIndex.Add("Right", 0x27);
            keyCodeToIndex.Add("Down", 0x28);
            keyCodeToIndex.Add("Select", 0x29);
            keyCodeToIndex.Add("Print", 0x2A);
            keyCodeToIndex.Add("Execute", 0x2B);
            keyCodeToIndex.Add("Snapshot", 0x2C);
            keyCodeToIndex.Add("Insert", 0x2D);
            keyCodeToIndex.Add("Delete", 0x2E);
            keyCodeToIndex.Add("Help", 0x2F);
            keyCodeToIndex.Add("Zero", 0x30);
            keyCodeToIndex.Add("One", 0x31);
            keyCodeToIndex.Add("Two", 0x32);
            keyCodeToIndex.Add("Three", 0x33);
            keyCodeToIndex.Add("Four", 0x34);
            keyCodeToIndex.Add("Five", 0x35);
            keyCodeToIndex.Add("Six", 0x36);
            keyCodeToIndex.Add("Seven", 0x37);
            keyCodeToIndex.Add("Eight", 0x38);
            keyCodeToIndex.Add("Nine", 0x39);
            keyCodeToIndex.Add("A", 0x41);
            keyCodeToIndex.Add("B", 0x42);
            keyCodeToIndex.Add("C", 0x43);
            keyCodeToIndex.Add("D", 0x44);
            keyCodeToIndex.Add("E", 0x45);
            keyCodeToIndex.Add("F", 0x46);
            keyCodeToIndex.Add("G", 0x47);
            keyCodeToIndex.Add("H", 0x48);
            keyCodeToIndex.Add("I", 0x49);
            keyCodeToIndex.Add("J", 0x4A);
            keyCodeToIndex.Add("K", 0x4B);
            keyCodeToIndex.Add("L", 0x4C);
            keyCodeToIndex.Add("M", 0x4D);
            keyCodeToIndex.Add("N", 0x4E);
            keyCodeToIndex.Add("O", 0x4F);
            keyCodeToIndex.Add("P", 0x50);
            keyCodeToIndex.Add("Q", 0x51);
            keyCodeToIndex.Add("R", 0x52);
            keyCodeToIndex.Add("S", 0x53);
            keyCodeToIndex.Add("T", 0x54);
            keyCodeToIndex.Add("U", 0x55);
            keyCodeToIndex.Add("V", 0x56);
            keyCodeToIndex.Add("W", 0x57);
            keyCodeToIndex.Add("X", 0x58);
            keyCodeToIndex.Add("Y", 0x59);
            keyCodeToIndex.Add("Z", 0x5A);
            keyCodeToIndex.Add("Lwin", 0x5B);
            keyCodeToIndex.Add("Rwin", 0x5C);
            keyCodeToIndex.Add("Apps", 0x5D);
            keyCodeToIndex.Add("Sleep", 0x5F);
            keyCodeToIndex.Add("Numpad0", 0x60);
            keyCodeToIndex.Add("Numpad1", 0x61);
            keyCodeToIndex.Add("Numpad2", 0x62);
            keyCodeToIndex.Add("Numpad3", 0x63);
            keyCodeToIndex.Add("Numpad4", 0x64);
            keyCodeToIndex.Add("Numpad5", 0x65);
            keyCodeToIndex.Add("Numpad6", 0x66);
            keyCodeToIndex.Add("Numpad7", 0x67);
            keyCodeToIndex.Add("Numpad8", 0x68);
            keyCodeToIndex.Add("Numpad9", 0x69);
            keyCodeToIndex.Add("Multiply", 0x6A);
            keyCodeToIndex.Add("Add", 0x6B);
            keyCodeToIndex.Add("Separator", 0x6C);
            keyCodeToIndex.Add("Subtract", 0x6D);
            keyCodeToIndex.Add("Decimal", 0x6E);
            keyCodeToIndex.Add("Divide", 0x6F);
            keyCodeToIndex.Add("F1", 0x70);
            keyCodeToIndex.Add("F2", 0x71);
            keyCodeToIndex.Add("F3", 0x72);
            keyCodeToIndex.Add("F4", 0x73);
            keyCodeToIndex.Add("F5", 0x74);
            keyCodeToIndex.Add("F6", 0x75);
            keyCodeToIndex.Add("F7", 0x76);
            keyCodeToIndex.Add("F8", 0x77);
            keyCodeToIndex.Add("F9", 0x78);
            keyCodeToIndex.Add("F10", 0x79);
            keyCodeToIndex.Add("F11", 0x7A);
            keyCodeToIndex.Add("F12", 0x7B);
            keyCodeToIndex.Add("F13", 0x7C);
            keyCodeToIndex.Add("F14", 0x7D);
            keyCodeToIndex.Add("F15", 0x7E);
            keyCodeToIndex.Add("F16", 0x7F);
            keyCodeToIndex.Add("F17", 0x80);
            keyCodeToIndex.Add("F18", 0x81);
            keyCodeToIndex.Add("F19", 0x82);
            keyCodeToIndex.Add("F20", 0x83);
            keyCodeToIndex.Add("F21", 0x84);
            keyCodeToIndex.Add("F22", 0x85);
            keyCodeToIndex.Add("F23", 0x86);
            keyCodeToIndex.Add("F24", 0x87);
            keyCodeToIndex.Add("Numlock", 0x90);
            keyCodeToIndex.Add("Scroll", 0x91);
            keyCodeToIndex.Add("Lshift", 0xA0);
            keyCodeToIndex.Add("Rshift", 0xA1);
            keyCodeToIndex.Add("Lcontrol", 0xA2);
            keyCodeToIndex.Add("Rcontrol", 0xA3);
            keyCodeToIndex.Add("Lmenu", 0xA4);
            keyCodeToIndex.Add("Rmenu", 0xA5);
            keyCodeToIndex.Add("BrowserBack", 0xA6);
            keyCodeToIndex.Add("BrowserForward", 0xA7);
            keyCodeToIndex.Add("BrowserRefresh", 0xA8);
            keyCodeToIndex.Add("BrowserStop", 0xA9);
            keyCodeToIndex.Add("BrowserSearch", 0xAA);
            keyCodeToIndex.Add("BrowserFavorites", 0xAB);
            keyCodeToIndex.Add("BrowserHome", 0xAC);
            keyCodeToIndex.Add("VolumeMute", 0xAD);
            keyCodeToIndex.Add("VolumeDown", 0xAE);
            keyCodeToIndex.Add("VolumeUp", 0xAF);
            keyCodeToIndex.Add("MediaNextTrack", 0xB0);
            keyCodeToIndex.Add("MediaPrevTrack", 0xB1);
            keyCodeToIndex.Add("MediaStop", 0xB2);
            keyCodeToIndex.Add("MediaPlayPause", 0xB3);
            keyCodeToIndex.Add("LaunchMail", 0xB4);
            keyCodeToIndex.Add("LaunchMediaSelect", 0xB5);
            keyCodeToIndex.Add("LaunchApp1", 0xB6);
            keyCodeToIndex.Add("LaunchApp2", 0xB7);
            keyCodeToIndex.Add("Oem1", 0xBA);
            keyCodeToIndex.Add("OemPlus", 0xBB);
            keyCodeToIndex.Add("OemComma", 0xBC);
            keyCodeToIndex.Add("OemMinus", 0xBD);
            keyCodeToIndex.Add("OemPeriod", 0xBE);
            keyCodeToIndex.Add("Oem2", 0xBF);
            keyCodeToIndex.Add("Oem3", 0xC0);
            keyCodeToIndex.Add("Oem4", 0xDB);
            keyCodeToIndex.Add("Oem5", 0xDC);
            keyCodeToIndex.Add("Oem6", 0xDD);
            keyCodeToIndex.Add("Oem7", 0xDE);
            keyCodeToIndex.Add("Oem8", 0xDF);
            keyCodeToIndex.Add("Oem102", 0xE2);
            keyCodeToIndex.Add("Processkey", 0xE5);
            keyCodeToIndex.Add("Packet", 0xE7);
            keyCodeToIndex.Add("Attn", 0xF6);
            keyCodeToIndex.Add("Crsel", 0xF7);
            keyCodeToIndex.Add("Exsel", 0xF8);
            keyCodeToIndex.Add("Ereof", 0xF9);
            keyCodeToIndex.Add("Play", 0xFA);
            keyCodeToIndex.Add("Zoom", 0xFB);
            keyCodeToIndex.Add("Noname", 0xFC);
            keyCodeToIndex.Add("Pa1", 0xFD);
            keyCodeToIndex.Add("OemClear", 0xFE);
            keyCodeToIndex.Add("Invalid", -1);
            #endregion

            foreach (var pair in keyCodeToIndex)
                indexToKeyCode.Add(pair.Value, pair.Key);
        }
        
        /// <summary>
        ///     Enumerates <c>VirtualKeyCode</c> and it's <c>string</c> representation.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<VirtualKeyCode, string>> EnumerateVirtualKeyCodes()
        {
            foreach (var pair in keyCodeToIndex)
                yield return new KeyValuePair<VirtualKeyCode, string>((VirtualKeyCode)pair.Value, pair.Key);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents a <c>VirtualKeyCode</c>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>A <see cref="System.String" /> that represents a <c>VirtualKeyCode</c>.</returns>
        public static string ToString(VirtualKeyCode code)
        {
            int index = (int)code;

            if (indexToKeyCode.ContainsKey(index)) return indexToKeyCode[index];

            return "Invalid";
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(int index)
        {
            if (indexToKeyCode.ContainsKey(index)) return indexToKeyCode[index];

            return "Invalid";
        }

        /// <summary>
        ///     Converts a string to it's corresponding VirtualKeyCode
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static VirtualKeyCode ToVirtualKeyCode(string name)
        {
            if (string.IsNullOrEmpty(name)) return VirtualKeyCode.Invalid;

            if (keyCodeToIndex.ContainsKey(name)) return (VirtualKeyCode)keyCodeToIndex[name];

            return VirtualKeyCode.Invalid;
        }

        /// <summary>
        ///     To the virtual key code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static VirtualKeyCode ToVirtualKeyCode(int code)
        {
            return (VirtualKeyCode)code;
        }
    }
}