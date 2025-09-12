using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Business;
using TaskManager.Persistence.Json;

namespace TaskManager.Tests
{
    /// <summary>
    ///     Tests für die Geschäftslogik des TaskService.
    /// </summary>
    [TestClass]
    public sealed class TaskServiceTests
    {
        /// <summary>
        ///     Prüft grundlegende Service-Operationen (Add, Toggle, Delete, Save).
        /// </summary>
        [TestMethod]
        public void Add_Toggle_Delete_Workflow()
        {
            Assert.Inconclusive("Iteration 2–4,8: Implementieren Sie Add/Toggle/Delete/Save/GetAll im Service/Repository und schreiben Sie die Asserts analog zur finalen Lösung.");
        }

        /// <summary>
        ///     Add mit DueDate speichert dieses korrekt ab.
        /// </summary>
        [TestMethod]
        public void Add_WithDueDate_SetsDueDate()
        {
            Assert.Inconclusive("Iteration 2/5: Implementieren Sie Add mit DueDate und prüfen Sie die gesetzte Fälligkeit.");
        }

        /// <summary>
        ///     Delete für eine nicht vorhandene ID liefert false.
        /// </summary>
        [TestMethod]
        public void Delete_ShouldReturnFalse_ForUnknownId()
        {
            Assert.Inconclusive("Iteration 3: Delete(Guid) für unbekannte ID sollte false liefern – Test implementieren.");
        }

        /// <summary>
        ///     Reload lädt den Zustand wieder aus dem Repository.
        /// </summary>
        [TestMethod]
        public void Reload_ShouldReload_FromRepository()
        {
            Assert.Inconclusive("Iteration 8: Save/Reload-Tests implementieren (Roundtrip über Repository).");
        }
    }
}