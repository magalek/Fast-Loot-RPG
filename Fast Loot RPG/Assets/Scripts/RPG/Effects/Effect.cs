using RPG.Entities;

namespace RPG.Effects {
    public class Effect 
    {
        public Stats Stats;    
        private Character target;
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

        public Effect(Stats stats, int _duration, Character _target)
        {
            Stats = stats;
            duration = _duration;
            target = _target;
        }
    }
}
