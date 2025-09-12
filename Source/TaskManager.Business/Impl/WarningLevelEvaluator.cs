using TaskManager.Model;

namespace TaskManager.Business
{
    /// <summary>
    ///     Stub-Evaluator für Warnstufen – Implementierung in späterer Iteration.
    /// </summary>
    public sealed class WarningLevelEvaluator : IWarningLevelEvaluator
    {
        private readonly int _redThreshold;
        private readonly int _yellowThreshold;

        /// <summary>
        ///     Erstellt eine neue Instanz des <see cref="WarningLevelEvaluator" />.
        /// </summary>
        public WarningLevelEvaluator(int yellowThreshold = 5, int redThreshold = 10)
        {
            _yellowThreshold = yellowThreshold;
            _redThreshold = redThreshold;
        }

        /// <inheritdoc />
        public WarningLevel Evaluate(int count)
        {
            throw new NotImplementedException("Iteration 7: WarningLevelEvaluator.Evaluate(open) implementieren (Green/Yellow/Red je nach Schwellwerten).");
        }
    }
}