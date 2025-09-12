using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Business;
using TaskManager.Model;

namespace TaskManager.Tests
{
    /// <summary>
    ///     Tests für den Warnstufen-Evaluator.
    /// </summary>
    [TestClass]
    public sealed class WarningLevelEvaluatorTests
    {
        /// <summary>
        ///     Ungültige Schwellwerte führen zu einer ArgumentException.
        /// </summary>
        [TestMethod]
        public void Ctor_ShouldThrow_OnInvalidThresholds()
        {
            Assert.Inconclusive("Iteration 7: Validierung der Schwellwerte im Konstruktor testen, sobald implementiert.");
        }

        /// <summary>
        ///     Prüft Grenzwerte und Zuordnungen.
        /// </summary>
        [TestMethod]
        public void Evaluate_ShouldRespectThresholds()
        {
            Assert.Inconclusive("Iteration 7: Grenzwert-Tests für WarningLevelEvaluator implementieren.");
        }
    }
}