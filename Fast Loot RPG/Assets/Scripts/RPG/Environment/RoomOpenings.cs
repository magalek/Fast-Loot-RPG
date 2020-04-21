using System;

namespace RPG.Environment {
    [Flags]
    public enum RoomOpenings {
        None = 0,    //0000
        Top = 1,     //0001
        Right = 2,   //0010
        Down = 4,    //0100    
        Left = 8,    //1000
    }
}