using System;
using UnityEngine;

namespace RPG.Statistics {
    public interface IStatistic {
        event Action Changed;
        int Current { get; set; }
        void Init();
        void Add(int amount);
        void Subtract(int amount);
    }
}