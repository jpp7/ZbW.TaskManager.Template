using TaskManager.Model;

namespace TaskManager.UI.WinForms
{
    /// <summary>
    ///     Dialog zum Bearbeiten einer einzelnen Aufgabe.
    /// </summary>
    public sealed class EditDialog : Form
    {
        private readonly TaskItem _taskItem;
        private CheckBox _completedCheckBox = null!;
        private CheckBox _dueDateEnabledCheckBox = null!;
        private DateTimePicker _dueDatePicker = null!;
        private Button _okButton = null!, _cancelButton = null!;
        private TextBox _titleTextBox = null!;

        /// <summary>
        ///     Erstellt den Bearbeitungsdialog für die angegebene Aufgabe.
        /// </summary>
        /// <param name="item">Die zu bearbeitende Aufgabe.</param>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="item" /> null ist.</exception>
        public EditDialog(TaskItem item)
        {
            _taskItem = item ?? throw new ArgumentNullException(nameof(item));
            Build();
            LoadData();
        }

        /// <summary>
        ///     Übernimmt die Eingaben aus dem Dialog in das Task-Objekt.
        /// </summary>
        /// <returns>True, wenn die Eingaben gültig waren; sonst false.</returns>
        private bool Apply()
        {
            var title = _titleTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Bitte Titel eingeben.");
                return false;
            }

            _taskItem.Title = title;
            _taskItem.IsCompleted = _completedCheckBox.Checked;
            _taskItem.DueDate = _dueDateEnabledCheckBox.Checked ? _dueDatePicker.Value.Date : (DateTime?)null;
            return true;
        }

        /// <summary>
        ///     Baut die Steuerelemente des Dialogs auf und verdrahtet Ereignisse.
        /// </summary>
        private void Build()
        {
            Width = 400;
            Height = 200;
            Text = "Edit Task";
            StartPosition = FormStartPosition.CenterParent;
            _titleTextBox = new TextBox { Left = 12, Top = 12, Width = 350 };
            _dueDatePicker = new DateTimePicker { Left = 12, Top = 41, Width = 200 };
            _dueDateEnabledCheckBox = new CheckBox { Left = 220, Top = 45, Text = "Due setzen" };
            _completedCheckBox = new CheckBox { Left = 12, Top = 70, Text = "Erledigt" };
            _okButton = new Button { Left = 206, Top = 110, Text = "OK", DialogResult = DialogResult.OK };
            _cancelButton = new Button
                { Left = 287, Top = 110, Text = "Abbrechen", DialogResult = DialogResult.Cancel };
            _okButton.Click += (s, e) =>
            {
                if (!Apply())
                {
                    DialogResult = DialogResult.None;
                }
            };
            Controls.AddRange(new Control[]
            {
                _titleTextBox, _dueDatePicker, _dueDateEnabledCheckBox, _completedCheckBox, _okButton, _cancelButton
            });
            AcceptButton = _okButton;
            CancelButton = _cancelButton;
        }

        /// <summary>
        ///     Lädt die Daten der Aufgabe in die Dialogfelder.
        /// </summary>
        private void LoadData()
        {
            _titleTextBox.Text = _taskItem.Title;
            if (_taskItem.DueDate.HasValue)
            {
                _dueDatePicker.Value = _taskItem.DueDate.Value;
                _dueDateEnabledCheckBox.Checked = true;
            }

            _completedCheckBox.Checked = _taskItem.IsCompleted;
        }
    }
}