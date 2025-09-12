using TaskManager.Model;

namespace TaskManager.Business
{
    /// <summary>
    ///     Bewertet den Warnstatus basierend auf der Anzahl offener Aufgaben.
    /// </summary>
    public interface IWarningLevelEvaluator
    {
        /// <summary>
        ///     Ermittelt den Warnstatus für die angegebene Anzahl offener Tasks.
        /// </summary>
        /// <param name="openTaskCount">Anzahl offener Tasks.</param>
        /// <returns>Der berechnete Warnstatus.</returns>
        WarningLevel Evaluate(int openTaskCount);
    }
}