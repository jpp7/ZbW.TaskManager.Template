namespace TaskManager.Model
{
    /// <summary>
    ///     Repräsentiert eine einzelne Aufgabe mit Titel, Fälligkeitsdatum und Status.
    /// </summary>
    public sealed class TaskItem
    {
        private string _title = string.Empty;

        /// <summary>
        ///     Erstellt eine neue Aufgabe mit dem angegebenen Titel.
        /// </summary>
        /// <param name="title">Der Titel der Aufgabe (darf nicht leer sein).</param>
        /// <exception cref="ArgumentException">Wird ausgelöst, wenn <paramref name="title" /> leer ist.</exception>
        public TaskItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            IsCompleted = false;
        }

        /// <summary>
        ///     Optionales Fälligkeitsdatum der Aufgabe.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        ///     Eindeutige Kennung der Aufgabe.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     Gibt an, ob die Aufgabe erledigt ist.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     Der Titel der Aufgabe. Darf nicht leer sein; führende/trailing Leerzeichen werden entfernt.
        /// </summary>
        /// <exception cref="ArgumentException">Wird ausgelöst, wenn ein leerer Titel gesetzt wird.</exception>
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title darf nicht leer sein.");
                }

                _title = value.Trim();
            }
        }
    }
}