namespace LowLevelInput.Hooks
{
    /// <summary>
    ///     An enumeration of all VirtualKeyCodes on Windows
    /// </summary>
    public enum VirtualKeyCode
    {
        /// <summary>
        ///     The invalid
        /// </summary>
        Invalid = -1,

        /// <summary>
        ///     The hotkey
        /// </summary>
        Hotkey = 0x0,

        /// <summary>
        ///     Left mouse button
        /// </summary>
        Lbutton = 0x01,

        /// <summary>
        ///     Right mouse button
        /// </summary>
        Rbutton = 0x02,

        /// <summary>
        ///     Control-break processing
        /// </summary>
        Cancel = 0x03,

        /// <summary>
        ///     Middle mouse button (three-button mouse)
        /// </summary>
        Mbutton = 0x04,

        /// <summary>
        ///     Windows 2000/XP: X1 mouse button
        /// </summary>
        Xbutton1 = 0x05,

        /// <summary>
        ///     Windows 2000/XP: X2 mouse button
        /// </summary>
        Xbutton2 = 0x06,

        /// <summary>
        ///     BACKSPACE key
        /// </summary>
        Back = 0x08,

        /// <summary>
        ///     TAB key
        /// </summary>
        Tab = 0x09,

        /// <summary>
        ///     CLEAR key
        /// </summary>
        Clear = 0x0C,

        /// <summary>
        ///     ENTER key
        /// </summary>
        Return = 0x0D,

        /// <summary>
        ///     SHIFT key
        /// </summary>
        Shift = 0x10,

        /// <summary>
        ///     CTRL key
        /// </summary>
        Control = 0x11,

        /// <summary>
        ///     ALT key
        /// </summary>
        Menu = 0x12,

        /// <summary>
        ///     PAUSE key
        /// </summary>
        Pause = 0x13,

        /// <summary>
        ///     CAPS LOCK key
        /// </summary>
        Capital = 0x14,

        /// <summary>
        ///     Input Method Editor (IME) Kana mode
        /// </summary>
        Kana = 0x15,

        /// <summary>
        ///     IME Hangul mode
        /// </summary>
        Hangul = 0x16,

        /// <summary>
        ///     IME Junja mode
        /// </summary>
        Junja = 0x17,

        /// <summary>
        ///     IME final mode
        /// </summary>
        Final = 0x18,

        /// <summary>
        ///     IME Hanja mode
        /// </summary>
        Hanja = 0x19,

        /// <summary>
        ///     IME Kanji mode
        /// </summary>
        Kanji = 0x1A,

        /// <summary>
        ///     ESC key
        /// </summary>
        Escape = 0x1B,

        /// <summary>
        ///     IME convert
        /// </summary>
        Convert = 0x1C,

        /// <summary>
        ///     IME nonconvert
        /// </summary>
        Nonconvert = 0x1D,

        /// <summary>
        ///     IME accept
        /// </summary>
        Accept = 0x1E,

        /// <summary>
        ///     IME mode change request
        /// </summary>
        Modechange = 0x1F,

        /// <summary>
        ///     SPACEBAR
        /// </summary>
        Space = 0x20,

        /// <summary>
        ///     PAGE UP key
        /// </summary>
        Prior = 0x21,

        /// <summary>
        ///     PAGE DOWN key
        /// </summary>
        Next = 0x22,

        /// <summary>
        ///     END key
        /// </summary>
        End = 0x23,

        /// <summary>
        ///     HOME key
        /// </summary>
        Home = 0x24,

        /// <summary>
        ///     LEFT ARROW key
        /// </summary>
        Left = 0x25,

        /// <summary>
        ///     UP ARROW key
        /// </summary>
        Up = 0x26,

        /// <summary>
        ///     RIGHT ARROW key
        /// </summary>
        Right = 0x27,

        /// <summary>
        ///     DOWN ARROW key
        /// </summary>
        Down = 0x28,

        /// <summary>
        ///     SELECT key
        /// </summary>
        Select = 0x29,

        /// <summary>
        ///     PRINT key
        /// </summary>
        Print = 0x2A,

        /// <summary>
        ///     EXECUTE key
        /// </summary>
        Execute = 0x2B,

        /// <summary>
        ///     PRINT SCREEN key
        /// </summary>
        Snapshot = 0x2C,

        /// <summary>
        ///     INS key
        /// </summary>
        Insert = 0x2D,

        /// <summary>
        ///     DEL key
        /// </summary>
        Delete = 0x2E,

        /// <summary>
        ///     HELP key
        /// </summary>
        Help = 0x2F,

        /// <summary>
        ///     0 key
        /// </summary>
        Zero = 0x30,

        /// <summary>
        ///     1 key
        /// </summary>
        One = 0x31,

        /// <summary>
        ///     2 key
        /// </summary>
        Two = 0x32,

        /// <summary>
        ///     3 key
        /// </summary>
        Three = 0x33,

        /// <summary>
        ///     4 key
        /// </summary>
        Four = 0x34,

        /// <summary>
        ///     5 key
        /// </summary>
        Five = 0x35,

        /// <summary>
        ///     6 key
        /// </summary>
        Six = 0x36,

        /// <summary>
        ///     7 key
        /// </summary>
        Seven = 0x37,

        /// <summary>
        ///     8 key
        /// </summary>
        Eight = 0x38,

        /// <summary>
        ///     9 key
        /// </summary>
        Nine = 0x39,

        /// <summary>
        ///     A key
        /// </summary>
        A = 0x41,

        /// <summary>
        ///     B key
        /// </summary>
        B = 0x42,

        /// <summary>
        ///     C key
        /// </summary>
        C = 0x43,

        /// <summary>
        ///     D key
        /// </summary>
        D = 0x44,

        /// <summary>
        ///     E key
        /// </summary>
        E = 0x45,

        /// <summary>
        ///     F key
        /// </summary>
        F = 0x46,

        /// <summary>
        ///     G key
        /// </summary>
        G = 0x47,

        /// <summary>
        ///     H key
        /// </summary>
        H = 0x48,

        /// <summary>
        ///     I key
        /// </summary>
        I = 0x49,

        /// <summary>
        ///     J key
        /// </summary>
        J = 0x4A,

        /// <summary>
        ///     K key
        /// </summary>
        K = 0x4B,

        /// <summary>
        ///     L key
        /// </summary>
        L = 0x4C,

        /// <summary>
        ///     M key
        /// </summary>
        M = 0x4D,

        /// <summary>
        ///     N key
        /// </summary>
        N = 0x4E,

        /// <summary>
        ///     O key
        /// </summary>
        O = 0x4F,

        /// <summary>
        ///     P key
        /// </summary>
        P = 0x50,

        /// <summary>
        ///     Q key
        /// </summary>
        Q = 0x51,

        /// <summary>
        ///     R key
        /// </summary>
        R = 0x52,

        /// <summary>
        ///     S key
        /// </summary>
        S = 0x53,

        /// <summary>
        ///     T key
        /// </summary>
        T = 0x54,

        /// <summary>
        ///     U key
        /// </summary>
        U = 0x55,

        /// <summary>
        ///     V key
        /// </summary>
        V = 0x56,

        /// <summary>
        ///     W key
        /// </summary>
        W = 0x57,

        /// <summary>
        ///     X key
        /// </summary>
        X = 0x58,

        /// <summary>
        ///     Y key
        /// </summary>
        Y = 0x59,

        /// <summary>
        ///     Z key
        /// </summary>
        Z = 0x5A,

        /// <summary>
        ///     Left Windows key (Microsoft Natural keyboard)
        /// </summary>
        Lwin = 0x5B,

        /// <summary>
        ///     Right Windows key (Natural keyboard)
        /// </summary>
        Rwin = 0x5C,

        /// <summary>
        ///     Applications key (Natural keyboard)
        /// </summary>
        Apps = 0x5D,

        /// <summary>
        ///     Computer Sleep key
        /// </summary>
        Sleep = 0x5F,

        /// <summary>
        ///     Numeric keypad 0 key
        /// </summary>
        Numpad0 = 0x60,

        /// <summary>
        ///     Numeric keypad 1 key
        /// </summary>
        Numpad1 = 0x61,

        /// <summary>
        ///     Numeric keypad 2 key
        /// </summary>
        Numpad2 = 0x62,

        /// <summary>
        ///     Numeric keypad 3 key
        /// </summary>
        Numpad3 = 0x63,

        /// <summary>
        ///     Numeric keypad 4 key
        /// </summary>
        Numpad4 = 0x64,

        /// <summary>
        ///     Numeric keypad 5 key
        /// </summary>
        Numpad5 = 0x65,

        /// <summary>
        ///     Numeric keypad 6 key
        /// </summary>
        Numpad6 = 0x66,

        /// <summary>
        ///     Numeric keypad 7 key
        /// </summary>
        Numpad7 = 0x67,

        /// <summary>
        ///     Numeric keypad 8 key
        /// </summary>
        Numpad8 = 0x68,

        /// <summary>
        ///     Numeric keypad 9 key
        /// </summary>
        Numpad9 = 0x69,

        /// <summary>
        ///     Multiply key
        /// </summary>
        Multiply = 0x6A,

        /// <summary>
        ///     Add key
        /// </summary>
        Add = 0x6B,

        /// <summary>
        ///     Separator key
        /// </summary>
        Separator = 0x6C,

        /// <summary>
        ///     Subtract key
        /// </summary>
        Subtract = 0x6D,

        /// <summary>
        ///     Decimal key
        /// </summary>
        Decimal = 0x6E,

        /// <summary>
        ///     Divide key
        /// </summary>
        Divide = 0x6F,

        /// <summary>
        ///     F1 key
        /// </summary>
        F1 = 0x70,

        /// <summary>
        ///     F2 key
        /// </summary>
        F2 = 0x71,

        /// <summary>
        ///     F3 key
        /// </summary>
        F3 = 0x72,

        /// <summary>
        ///     F4 key
        /// </summary>
        F4 = 0x73,

        /// <summary>
        ///     F5 key
        /// </summary>
        F5 = 0x74,

        /// <summary>
        ///     F6 key
        /// </summary>
        F6 = 0x75,

        /// <summary>
        ///     F7 key
        /// </summary>
        F7 = 0x76,

        /// <summary>
        ///     F8 key
        /// </summary>
        F8 = 0x77,

        /// <summary>
        ///     F9 key
        /// </summary>
        F9 = 0x78,

        /// <summary>
        ///     F10 key
        /// </summary>
        F10 = 0x79,

        /// <summary>
        ///     F11 key
        /// </summary>
        F11 = 0x7A,

        /// <summary>
        ///     F12 key
        /// </summary>
        F12 = 0x7B,

        /// <summary>
        ///     F13 key
        /// </summary>
        F13 = 0x7C,

        /// <summary>
        ///     F14 key
        /// </summary>
        F14 = 0x7D,

        /// <summary>
        ///     F15 key
        /// </summary>
        F15 = 0x7E,

        /// <summary>
        ///     F16 key
        /// </summary>
        F16 = 0x7F,

        /// <summary>
        ///     F17 key
        /// </summary>
        F17 = 0x80,

        /// <summary>
        ///     F18 key
        /// </summary>
        F18 = 0x81,

        /// <summary>
        ///     F19 key
        /// </summary>
        F19 = 0x82,

        /// <summary>
        ///     F20 key
        /// </summary>
        F20 = 0x83,

        /// <summary>
        ///     F21 key
        /// </summary>
        F21 = 0x84,

        /// <summary>
        ///     F22 key, (PPC only) Key used to lock device.
        /// </summary>
        F22 = 0x85,

        /// <summary>
        ///     F23 key
        /// </summary>
        F23 = 0x86,

        /// <summary>
        ///     F24 key
        /// </summary>
        F24 = 0x87,

        /// <summary>
        ///     NUM LOCK key
        /// </summary>
        Numlock = 0x90,

        /// <summary>
        ///     SCROLL LOCK key
        /// </summary>
        Scroll = 0x91,

        /// <summary>
        ///     Left SHIFT key
        /// </summary>
        Lshift = 0xA0,

        /// <summary>
        ///     Right SHIFT key
        /// </summary>
        Rshift = 0xA1,

        /// <summary>
        ///     Left CONTROL key
        /// </summary>
        Lcontrol = 0xA2,

        /// <summary>
        ///     Right CONTROL key
        /// </summary>
        Rcontrol = 0xA3,

        /// <summary>
        ///     Left MENU key
        /// </summary>
        Lmenu = 0xA4,

        /// <summary>
        ///     Right MENU key
        /// </summary>
        Rmenu = 0xA5,

        /// <summary>
        ///     Windows 2000/XP: Browser Back key
        /// </summary>
        BrowserBack = 0xA6,

        /// <summary>
        ///     Windows 2000/XP: Browser Forward key
        /// </summary>
        BrowserForward = 0xA7,

        /// <summary>
        ///     Windows 2000/XP: Browser Refresh key
        /// </summary>
        BrowserRefresh = 0xA8,

        /// <summary>
        ///     Windows 2000/XP: Browser Stop key
        /// </summary>
        BrowserStop = 0xA9,

        /// <summary>
        ///     Windows 2000/XP: Browser Search key
        /// </summary>
        BrowserSearch = 0xAA,

        /// <summary>
        ///     Windows 2000/XP: Browser Favorites key
        /// </summary>
        BrowserFavorites = 0xAB,

        /// <summary>
        ///     Windows 2000/XP: Browser Start and Home key
        /// </summary>
        BrowserHome = 0xAC,

        /// <summary>
        ///     Windows 2000/XP: Volume Mute key
        /// </summary>
        VolumeMute = 0xAD,

        /// <summary>
        ///     Windows 2000/XP: Volume Down key
        /// </summary>
        VolumeDown = 0xAE,

        /// <summary>
        ///     Windows 2000/XP: Volume Up key
        /// </summary>
        VolumeUp = 0xAF,

        /// <summary>
        ///     Windows 2000/XP: Next Track key
        /// </summary>
        MediaNextTrack = 0xB0,

        /// <summary>
        ///     Windows 2000/XP: Previous Track key
        /// </summary>
        MediaPrevTrack = 0xB1,

        /// <summary>
        ///     Windows 2000/XP: Stop Media key
        /// </summary>
        MediaStop = 0xB2,

        /// <summary>
        ///     Windows 2000/XP: Play/Pause Media key
        /// </summary>
        MediaPlayPause = 0xB3,

        /// <summary>
        ///     Windows 2000/XP: Start Mail key
        /// </summary>
        LaunchMail = 0xB4,

        /// <summary>
        ///     Windows 2000/XP: Select Media key
        /// </summary>
        LaunchMediaSelect = 0xB5,

        /// <summary>
        ///     Windows 2000/XP: Start Application 1 key
        /// </summary>
        LaunchApp1 = 0xB6,

        /// <summary>
        ///     Windows 2000/XP: Start Application 2 key
        /// </summary>
        LaunchApp2 = 0xB7,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem1 = 0xBA,

        /// <summary>
        ///     Windows 2000/XP: For any country/region, the '+' key
        /// </summary>
        OemPlus = 0xBB,

        /// <summary>
        ///     Windows 2000/XP: For any country/region, the ',' key
        /// </summary>
        OemComma = 0xBC,

        /// <summary>
        ///     Windows 2000/XP: For any country/region, the '-' key
        /// </summary>
        OemMinus = 0xBD,

        /// <summary>
        ///     Windows 2000/XP: For any country/region, the '.' key
        /// </summary>
        OemPeriod = 0xBE,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem2 = 0xBF,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem3 = 0xC0,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem4 = 0xDB,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem5 = 0xDC,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem6 = 0xDD,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem7 = 0xDE,

        /// <summary>
        ///     Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Oem8 = 0xDF,

        /// <summary>
        ///     Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
        /// </summary>
        Oem102 = 0xE2,

        /// <summary>
        ///     Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
        /// </summary>
        Processkey = 0xE5,

        /// <summary>
        ///     Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes.
        ///     The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more
        ///     information,
        ///     see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        /// </summary>
        Packet = 0xE7,

        /// <summary>
        ///     Attn key
        /// </summary>
        Attn = 0xF6,

        /// <summary>
        ///     CrSel key
        /// </summary>
        Crsel = 0xF7,

        /// <summary>
        ///     ExSel key
        /// </summary>
        Exsel = 0xF8,

        /// <summary>
        ///     Erase EOF key
        /// </summary>
        Ereof = 0xF9,

        /// <summary>
        ///     Play key
        /// </summary>
        Play = 0xFA,

        /// <summary>
        ///     Zoom key
        /// </summary>
        Zoom = 0xFB,

        /// <summary>
        ///     Reserved
        /// </summary>
        Noname = 0xFC,

        /// <summary>
        ///     PA1 key
        /// </summary>
        Pa1 = 0xFD,

        /// <summary>
        ///     Clear key
        /// </summary>
        OemClear = 0xFE
    }
}