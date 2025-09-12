using System.Text.Json;
using TaskManager.Model;

namespace TaskManager.Persistence.Json
{
    /// <summary>
    ///     Stub-Repository (JSON). Implementierung erfolgt in späteren Iterationen.
    /// </summary>
    public sealed class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        /// <summary>
        ///     Erstellt ein JSON-basiertes Repository.
        /// </summary>
        public JsonTaskRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("filePath leer.", nameof(filePath));
            }

            _filePath = filePath;
        }

        /// <inheritdoc />
        public IEnumerable<TaskItem> Load()
        {
            throw new NotImplementedException("Iteration 8: JsonTaskRepository.Load() implementieren (Datei lesen, JSON parsen, tolerant bei Fehlern -> leere Liste).");
        }

        /// <inheritdoc />
        public void Save(IEnumerable<TaskItem> items)
        {
            throw new NotImplementedException("Iteration 8: JsonTaskRepository.Save(items) implementieren (Argumente prüfen, JSON schreiben).");
        }
    }
}