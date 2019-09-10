using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect 
{
    private Statistics statistics;    
    private EffectManager target;
    private int duration;

    public int Duration
    {
        get
        {
            return duration;
        }
        set
        {
            if (value == 0)
                target.RemoveEffect(this);
            duration = value;          
        }
    }

    public Effect(Statistics _statistics, int _duration, EffectManager _target)
    {
        statistics = _statistics;
        duration = _duration;
        target = _target;
    }
}
