# ZbW TaskManager – Musterlösung (Studierendenprojekt)

Dieses Repository enthält eine einfache, aber vollständige Aufgabenverwaltung mit WinForms-UI, Business‑Logik, JSON‑Persistenz und Komponententests. Es ist als Musterlösung strukturiert und dient Studierenden als Referenz für Clean Code, Schichtenarchitektur, Benennungskonventionen und Testaufbau.

## Überblick
- UI: .NET 9 WinForms (TaskManager.UI.WinForms)
- Business: Geschäftslogik, klare Interfaces + Implementierungen (TaskManager.Business)
- Persistence: JSON‑Repository (TaskManager.Persistence.Json)
- Model: Domänenobjekte (TaskManager.Model)
- Tests: MSTest, in Klassen je Verantwortlichkeit aufgeteilt (TaskManager.Tests)

## Architektur & Projektstruktur
- Model (POCOs): TaskItem, WarningLevel
- Business:
  - ITaskService, IWarningLevelEvaluator
  - Impl/TaskService, Impl/WarningLevelEvaluator
- Persistence:
  - ITaskRepository
  - Impl/JsonTaskRepository
- UI:
  - MainForm (ListView mit Checkboxen, Doppelklick zum Bearbeiten)
  - EditDialog (Titel, optionales Fälligkeitsdatum, erledigt)
  - Program (Composition Root)

Prinzipien:
- Single Responsibility per Klasse/Datei
- Schnittstellen + Impl‑Ordner für konkrete Implementierungen
- Strikte Benennungskonventionen (PascalCase für Typen/Member, _camelCase Felder)
- Deutsche XML‑Doku für alle öffentlichen Typen/Member
- UI‑Elemente mit Präfixen gemäss Vorgabe (z. B. Cmd, Txt, Lst, Lbl, Pnl, Chk, Num, …)

## Tests
- Framework: MSTest (3.6.0)
- Abdeckung:
  - JsonTaskRepositoryTests: Laden/Speichern, Robustheit bei korrupten/fehlenden Dateien
  - TaskServiceTests: Add/Toggle/Delete/Reload, DueDate‑Handling
  - TaskItemTests: Titel‑Validierung/Trim, Defaults, DueDate nullbar
  - WarningLevelEvaluatorTests: Grenzwerte + ungültige Parameter
- 13/13 Tests grün

## Build & Run
- Voraussetzungen: .NET 9 SDK, Windows (WinForms)
- Build: dotnet build (oder in Rider/VS)
- Tests: dotnet test (oder Test Runner)
- Start: Projekt TaskManager.UI.WinForms ausführen
- Datenpfad: %APPDATA%\ZbW.TaskManager\tasks.json

Hinweis (Lernmodus): Diese Repository‑Variante ist ein Studenten‑Template ohne Build‑Symbole. Die Anwendung startet immer, und nicht implementierte Funktionen lösen zur Laufzeit eine NotImplementedException mit Hinweismeldung aus, die sagt, in welcher Iteration dies umgesetzt wird. Die Unit‑Tests für spätere Iterationen sind enthalten, geben aber Assert.Inconclusive aus, bis die entsprechende Iteration implementiert wurde. So sehen Studierende direkt, was als Nächstes zu tun ist.

## Code‑Qualität & Konventionen
- Nullable Reference Types aktiv
- Alle Warnungen bereinigt (inkl. CS86xx, CS1591; NU1603 gelöst; NETSDK1137 unterdrückt für WindowsDesktop SDK)
- Klares Event‑Handling in UI, AAA‑Kommentare in Tests

## Mögliche Erweiterungen (Empfehlungen für Studierende)
Diese Punkte sind bewusst nicht alle implementiert, eignen sich jedoch hervorragend als Übung oder für Bonus‑Features:

1. Dependency Injection (DI)
   - Ziel: Program.cs entkoppeln, Konstruktion über DI‑Container (z. B. Microsoft.Extensions.DependencyInjection).
   - Nutzen: Austausch von Implementierungen (Repository, Evaluator) wird trivial, bessere Testbarkeit.

2. Persistenzhärtung
   - Atomic Save: Erst in temporäre Datei schreiben, dann atomar ersetzen (z. B. File.Replace), um Datenverlust zu verhindern.
   - Fehlerbehandlung: Nutzerfreundliche Meldungen bei I/O‑Fehlern, Logging einführen (z. B. ILogger).

3. UI‑Verbesserungen
   - Überfällige Aufgaben visuell hervorheben (z. B. rote Schrift oder Icon, wenn DueDate < Today und nicht erledigt).
   - Sortier-/Filteroptionen (nur offene, nur überfällige, nach Datum/Titel sortieren).
   - Detail‑Ansicht mit Spalten (ListView im Details‑Modus mit Columns: Done, Title, DueDate).

4. Zusätzliche Business‑Regeln
   - Doppelte Titel verhindern (optional konfigurierbar).
   - Automatisches Archivieren erledigter Aufgaben (z. B. in separate Datei oder Sektion).
   - Warnschwellen aus Konfiguration laden (appsettings.json) statt harter Werte.

5. Alternative Repositories
   - In‑Memory‑Repository für Tests/Playground.
   - FileSystem‑Abstraktion (IFileSystem), um I/O in Tests leichter zu mocken.
   - Weitere Formate (CSV, SQLite mittels Dapper/EF Core – je nach Modulziel).

6. Test‑Erweiterungen
   - Property‑Based Tests (z. B. mit FsCheck) für robuste Eingabevarianten.
   - Tests für UI‑Logik in Presenter/Service extrahieren (falls MVP/MVVM eingeführt wird).
   - Code Coverage Bericht (coverlet + ReportGenerator) in CI anzeigen.

7. Tooling & Qualität
   - EditorConfig vervollständigen (z. B. Namenskonventionen, Formatierungsregeln, IDE‑Hints).
   - Roslyn‑Analyzers/StyleCop aktivieren und konfigurieren.
   - GitHub Actions/DevOps Pipeline: Build + Tests + Artefakt (ZIP) veröffentlichen.

8. Internationalisierung (i18n)
   - UI‑Texte in Ressourcen (.resx) auslagern, Lokalisierung ermöglichen (de/de‑CH, en‑US).

9. UX‑Details
   - Bestätigung beim Löschen (MessageBox mit Ja/Nein).
   - Tastenkürzel (Enter speichert, Esc schliesst, Del löscht Auswahl).
   - Persistente Fensterposition/Grösse (ApplicationSettings).

10. Fehlerrobustheit im Model
   - Weitere Validierungen (max. Titellänge, verbotene Zeichen) und passende Tests.

## Didaktische Hinweise
- Der aktuelle Stand zeigt sauberes Refactoring von "alles in einer Datei" hin zu:
  - Ein Typ pro Datei
  - Trennung Interface/Implementierung (Impl‑Ordner)
  - Konsistente Benennung + vollständige Doku
- Die vorgeschlagenen Erweiterungen sind so gewählt, dass sie schrittweise den Lernweg von Grundlagen (Schichten, Tests) zu fortgeschrittenen Themen (DI, Persistenzhärtung, CI, Analyzers) begleiten.

Viel Erfolg und viel Spass beim Erweitern!
