using TaskManager.Model;

namespace TaskManager.Persistence
{
    /// <summary>
    ///     Definiert die Schnittstelle zur Persistierung von Aufgaben.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        ///     Lädt alle Aufgaben aus dem Datenspeicher.
        /// </summary>
        IEnumerable<TaskItem> Load();

        /// <summary>
        ///     Speichert die angegebenen Aufgaben im Datenspeicher.
        /// </summary>
        /// <param name="items">Die zu speichernden Aufgaben.</param>
        void Save(IEnumerable<TaskItem> items);
    }
}