using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Model;
using TaskManager.Persistence.Json;

namespace TaskManager.Tests
{
    /// <summary>
    ///     Tests für das JSON-Repository (Laden/Speichern und Robustheit).
    /// </summary>
    [TestClass]
    public sealed class JsonTaskRepositoryTests
    {
        /// <summary>
        ///     Stellt sicher, dass das JSON-Repository bei korrupten Dateien keine Ausnahme wirft und leer lädt.
        /// </summary>
        [TestMethod]
        public void Load_ShouldReturnEmpty_OnCorruptFile()
        {
            Assert.Inconclusive("Iteration 8: Korruptes JSON -> leere Liste, keine Exception – Test implementieren.");
        }

        /// <summary>
        ///     Wenn die Datei nicht existiert, liefert Load eine leere Liste zurück.
        /// </summary>
        [TestMethod]
        public void Load_ShouldReturnEmpty_WhenFileMissing()
        {
            Assert.Inconclusive("Iteration 8: Fehlende Datei -> leere Liste – Test implementieren.");
        }

        /// <summary>
        ///     Speichern und anschliessendes Laden liefert die gleichen Daten zurück.
        /// </summary>
        [TestMethod]
        public void Save_Then_Load_Roundtrip_Works()
        {
            Assert.Inconclusive("Iteration 8: Save + Load Roundtrip – Test implementieren.");
        }
    }
}