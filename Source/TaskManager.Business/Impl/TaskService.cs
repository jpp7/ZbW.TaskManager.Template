using TaskManager.Model;
using TaskManager.Persistence;

namespace TaskManager.Business
{
    /// <summary>
    ///     Stub-Implementierung der Geschäftslogik – Methoden werden in späteren Iterationen von Studierenden implementiert.
    /// </summary>
    public sealed class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        /// <summary>
        ///     Erstellt eine neue Instanz des <see cref="TaskService" />.
        /// </summary>
        /// <param name="repository">Repository zur Persistierung von Aufgaben.</param>
        public TaskService(ITaskRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc />
        public TaskItem Add(string title, DateTime? dueDate)
        {
            throw new NotImplementedException("Iteration 2: TaskService.Add(title, dueDate) implementieren (Validierung, Trim, Anlegen, Rückgabe).");
        }

        /// <inheritdoc />
        public int CountOpen()
        {
            throw new NotImplementedException("Iteration 6: TaskService.CountOpen() implementieren (nur unerledigte zählen).");
        }

        /// <inheritdoc />
        public bool Delete(Guid id)
        {
            throw new NotImplementedException("Iteration 3: TaskService.Delete(id) implementieren (true/false je nach Fund).");
        }

        /// <inheritdoc />
        public IReadOnlyList<TaskItem> GetAll()
        {
            throw new NotImplementedException("Iteration 8: TaskService.GetAll() implementieren (interner Speicher, ReadOnly-View).");
        }

        /// <inheritdoc />
        public void Reload()
        {
            throw new NotImplementedException("Iteration 8: TaskService.Reload() implementieren (aus Repository laden).");
        }

        /// <inheritdoc />
        public void Save()
        {
            throw new NotImplementedException("Iteration 8: TaskService.Save() implementieren (in Repository speichern).");
        }

        /// <inheritdoc />
        public bool ToggleComplete(Guid id)
        {
            throw new NotImplementedException("Iteration 4: TaskService.ToggleComplete(id) implementieren (Status wechseln, true/false).");
        }
    }
}