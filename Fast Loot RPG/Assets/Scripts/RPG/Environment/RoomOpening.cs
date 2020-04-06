using System;

namespace RPG.Environment {
    [Flags]
    public enum RoomOpening {
        None = 0,
        Top = 1,
        Right = 2,
        Down = 4,
        Left = 8,
    }
}