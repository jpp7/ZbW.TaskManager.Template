using TaskManager.Model;

namespace TaskManager.Business
{
    /// <summary>
    ///     Bietet Geschäftslogik zum Verwalten von Aufgaben.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        ///     Fügt eine neue Aufgabe hinzu.
        /// </summary>
        /// <param name="title">Titel der Aufgabe.</param>
        /// <param name="dueDate">Optionales Fälligkeitsdatum.</param>
        /// <returns>Das hinzugefügte Task-Objekt.</returns>
        TaskItem Add(string title, DateTime? dueDate);

        /// <summary>
        ///     Liefert die Anzahl offener Aufgaben.
        /// </summary>
        int CountOpen();

        /// <summary>
        ///     Löscht eine Aufgabe anhand ihrer ID.
        /// </summary>
        /// <param name="id">Die eindeutige ID der Aufgabe.</param>
        /// <returns>True, wenn eine Aufgabe gelöscht wurde; sonst false.</returns>
        bool Delete(Guid id);

        /// <summary>
        ///     Liefert alle Aufgaben als schreibgeschützte Liste.
        /// </summary>
        IReadOnlyList<TaskItem> GetAll();

        /// <summary>
        ///     Lädt die Aufgaben erneut aus dem Repository.
        /// </summary>
        void Reload();

        /// <summary>
        ///     Speichert alle Aufgaben im Repository.
        /// </summary>
        void Save();

        /// <summary>
        ///     Wechselt den Erledigt-Status einer Aufgabe.
        /// </summary>
        /// <param name="id">Die eindeutige ID der Aufgabe.</param>
        /// <returns>True, wenn eine Aufgabe gefunden und aktualisiert wurde; sonst false.</returns>
        bool ToggleComplete(Guid id);
    }
}