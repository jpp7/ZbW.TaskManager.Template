namespace TaskManager.Model
{
    /// <summary>
    ///     Warnstufe, die den Status basierend auf der Anzahl offener Aufgaben beschreibt.
    /// </summary>
    public enum WarningLevel
    {
        /// <summary>
        ///     Alles im grünen Bereich.
        /// </summary>
        Green = 0,

        /// <summary>
        ///     Mittlere Auslastung – Aufmerksamkeit empfohlen.
        /// </summary>
        Yellow = 1,

        /// <summary>
        ///     Hohe Auslastung – Handlungsbedarf.
        /// </summary>
        Red = 2
    }
}