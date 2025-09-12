using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Model;

namespace TaskManager.Tests
{
    /// <summary>
    ///     Tests für das Datenmodell TaskItem.
    /// </summary>
    [TestClass]
    public sealed class TaskItemTests
    {
        /// <summary>
        ///     Standardwerte: IsCompleted=false, DueDate=null.
        /// </summary>
        [TestMethod]
        public void Defaults_ShouldBeSet()
        {
            // Arrange & Act
            var t = new TaskItem("X");

            // Assert
            Assert.IsFalse(t.IsCompleted);
            Assert.IsNull(t.DueDate);
        }

        /// <summary>
        ///     DueDate kann auf null gesetzt werden.
        /// </summary>
        [TestMethod]
        public void DueDate_CanBeNull()
        {
            // Arrange
            var t = new TaskItem("Y") { DueDate = DateTime.Today };

            // Act
            t.DueDate = null;

            // Assert
            Assert.IsNull(t.DueDate);
        }

        /// <summary>
        ///     Titel darf nicht leer sein (Ausnahme erwartet).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Title_MustNotBeEmpty()
        {
            // Arrange & Act
            _ = new TaskItem("");
        }

        /// <summary>
        ///     Titel wird beim Setzen getrimmt.
        /// </summary>
        [TestMethod]
        public void Title_ShouldBeTrimmed()
        {
            // Arrange & Act
            var t = new TaskItem("  Hello  ");

            // Assert
            Assert.AreEqual("Hello", t.Title);
        }
    }
}