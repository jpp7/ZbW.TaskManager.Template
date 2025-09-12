using TaskManager.Business;
using TaskManager.Model;

namespace TaskManager.UI.WinForms
{
    /// <summary>
    ///     Hauptfenster der Anwendung zum Verwalten und Anzeigen von Aufgaben.
    /// </summary>
    public sealed partial class MainForm : Form
    {
        private readonly IWarningLevelEvaluator _evaluator;
        private readonly ITaskService _service;

        /// <summary>
        ///     Erstellt das Hauptfenster und initialisiert Steuerungen und Datenbindung.
        /// </summary>
        /// <param name="svc">Dienst für Aufgabenverwaltung.</param>
        /// <param name="eval">Evaluator für Warnstufenanzeige.</param>
        public MainForm(ITaskService svc, IWarningLevelEvaluator eval)
        {
            _service = svc ?? throw new ArgumentNullException(nameof(svc));
            _evaluator = eval ?? throw new ArgumentNullException(nameof(eval));
            InitializeComponent();
            try
            {
                RefreshList();
            }
            catch (NotImplementedException)
            {
                // In der Vorlage: Falls Methoden noch nicht implementiert sind, neutral anzeigen.
                LstTasks.Items.Clear();
                LblCounter.Text = "Open: 0";
                PnlStatus.BackColor = Color.LightGreen;
            }
        }

        /// <summary>
        ///     Ereignishandler: Fügt eine neue Aufgabe hinzu.
        /// </summary>
        private void btnAdd_Click(object o, EventArgs a)
        {
            try
            {
                var t = TxtTitle.Text.Trim();
                if (string.IsNullOrWhiteSpace(t))
                {
                    MessageBox.Show("Bitte Titel eingeben.");
                    TxtTitle.Focus();
                    return;
                }
                DateTime? d = ChkDue.Checked ? DateDue.Value.Date : (DateTime?)null;
                _service.Add(t, d);
                TxtTitle.Clear();
                ChkDue.Checked = false;
                RefreshList();
                _service.Save();
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Ereignishandler: Löscht die ausgewählte Aufgabe.
        /// </summary>
        private void btnDelete_Click(object o, EventArgs a)
        {
            try
            {
                var sel = Selected();
                if (sel == null)
                {
                    return;
                }

                if (_service.Delete(sel.Id))
                {
                    RefreshList();
                    _service.Save();
                }
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Ereignishandler: Wechselt den Erledigt-Status der ausgewählten Aufgabe.
        /// </summary>
        private void btnToggle_Click(object o, EventArgs a)
        {
            try
            {
                var sel = Selected();
                if (sel == null)
                {
                    return;
                }
                if (_service.ToggleComplete(sel.Id))
                {
                    RefreshList();
                    _service.Save();
                }
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Ereignishandler: Öffnet den Bearbeitungsdialog für die ausgewählte Aufgabe.
        /// </summary>
        private void listTasks_DoubleClick(object o, EventArgs a)
        {
            try
            {
                var sel = Selected();
                if (sel == null)
                {
                    return;
                }

                using (var dlg = new EditDialog(sel))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        RefreshList();
                        _service.Save();
                    }
                }
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Ereignishandler: Checkbox einer ListView-Position ändert sich -> Task-Status toggeln und speichern.
        /// </summary>
        private void listTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                // Bestimme das betroffene TaskItem über Tag und synchronisiere den Service.
                var item = LstTasks.Items[e.Index];
                if (item?.Tag is TaskItem task)
                {
                    var shouldBeCompleted = e.NewValue == CheckState.Checked;
                    if (task.IsCompleted != shouldBeCompleted)
                    {
                        _service.ToggleComplete(task.Id);
                        _service.Save();
                        UpdateCounters();
                    }
                }
            }
            catch (NotImplementedException ex)
            {
                // Visuelle Rücknahme, Hinweis anzeigen.
                e.NewValue = e.CurrentValue;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Aktualisiert die Aufgabenliste und die Anzeige.
        /// </summary>
        private void RefreshList()
        {
            try
            {
                var items = _service.GetAll().ToList();
                LstTasks.Items.Clear();
                foreach (var it in items)
                {
                    var text = it.DueDate.HasValue ? $"{it.Title} (Due: {it.DueDate.Value.ToShortDateString()})" : it.Title;
                    var lvi = new ListViewItem(text) { Checked = it.IsCompleted, Tag = it };
                    LstTasks.Items.Add(lvi);
                }

                UpdateCounters();
            }
            catch (NotImplementedException)
            {
                LstTasks.Items.Clear();
                LblCounter.Text = "Open: 0";
                PnlStatus.BackColor = Color.LightGreen;
            }
        }

        /// <summary>
        ///     Liefert das aktuell ausgewählte Task-Objekt oder null.
        /// </summary>
        private TaskItem? Selected()
        {
            if (LstTasks.SelectedItems.Count == 0)
            {
                return null;
            }

            return LstTasks.SelectedItems[0].Tag as TaskItem;
        }

        /// <summary>
        ///     Aktualisiert die Anzeige der Zähler und den Warnstatus.
        /// </summary>
        private void UpdateCounters()
        {
            try
            {
                var open = _service.CountOpen();
                LblCounter.Text = $"Open: {open}";
                var lvl = _evaluator.Evaluate(open);
                PnlStatus.BackColor = lvl == WarningLevel.Green
                    ? Color.LightGreen
                    : (lvl == WarningLevel.Yellow ? Color.Khaki : Color.IndianRed);
            }
            catch (NotImplementedException)
            {
                LblCounter.Text = "Open: 0";
                PnlStatus.BackColor = Color.LightGreen;
            }
        }
    }
}