using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    class ControlType
    {
        public const int MOUSE = 0;
        public const int KEYBOARD = 1;
        public const int GAMEPAD = 2;

        public static string[] CONTROL_NAMES =
        {
            "MOUSE", "KEYBOARD", "GAMEPAD"
        };
    }
}
