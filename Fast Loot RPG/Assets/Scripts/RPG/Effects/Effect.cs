using RPG.Entities;

namespace RPG.Effects {
    public class Effect 
    {
        public Statistics statistics;    
        private Entity target;
        private int duration;

        public int Duration
        {
            get => duration;
            set
            {
                if (value == 0)
                    //target.effectsController.RemoveEffect(this);
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
}
