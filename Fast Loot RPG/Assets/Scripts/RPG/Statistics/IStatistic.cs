using System;
using UnityEngine;

namespace RPG.Statistics {
    public interface IStatistic<T> {
        event Action Changed;
        T Current { get; set; }
        void Init();
        void ChangeCurrentBy(T amount);
    }
}