namespace RPG.Entities {
    public interface IKillable {
        void AddHealth();
        void SubtractHealth();        
        void Kill();
    }
}