namespace RPG.Statistics {
    public interface IMaxStatistic<T> : IStatistic<T> {
        T Max { get; set; }
        void ChangeMaxBy(T amount);
    }
}