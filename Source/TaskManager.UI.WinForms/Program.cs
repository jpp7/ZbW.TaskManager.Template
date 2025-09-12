using TaskManager.Business;
using TaskManager.Persistence;
using TaskManager.Persistence.Json;

namespace TaskManager.UI.WinForms
{
    /// <summary>
    ///     Einstiegspunkt der WinForms-Anwendung.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///     Hauptmethode der Anwendung. Initialisiert die UI und startet das Hauptfenster.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ZbW.TaskManager");
            Directory.CreateDirectory(dir);
            var file = Path.Combine(dir, "tasks.json");
            ITaskRepository repo = new JsonTaskRepository(file);
            IWarningLevelEvaluator eval = new WarningLevelEvaluator(5, 10);
            ITaskService svc = new TaskService(repo);
            Application.Run(new MainForm(svc, eval));
        }
    }
}