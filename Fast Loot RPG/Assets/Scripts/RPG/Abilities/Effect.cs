using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using UnityEngine;

public class Effect 
{
    public Statistics statistics;    
    private Entity target;
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
                target.effectManager.RemoveEffect(this);
            duration = value;          
        }
    }

    public Effect(Statistics _statistics, int _duration, Entity _target)
    {
        statistics = _statistics;
        duration = _duration;
        target = _target;
    }
}
