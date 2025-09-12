# 📌 User Stories – Task Manager (Iteratives Vorgehen)

Stand: 2025-09-09

Dieses Dokument beschreibt alle User Stories für den Task Manager.
Jede Story enthält fachliche Akzeptanzkriterien (AK) und ergänzende technische Akzeptanzkriterien (TAK), damit klar ist, was genau zu implementieren ist.
Die Stories sind in 11 Iterationen organisiert. Spätere Iterationen sind als Stubs vorgesehen und werden von Studierenden umgesetzt. Die Anwendung läuft stets; nicht implementierte Funktionen melden sich mit einer klaren Fehlermeldung (inkl. Iterationshinweis).

---

## Iteration 1 – Grundstruktur & Domain‑Modell
**Story:**  Als Entwickler möchte ich eine klare Projektstruktur und ein minimales Domänenmodell, damit die Basis steht.

**AK (fachlich):**
- Projektstruktur vorhanden (Model, Business, Persistence, UI, Tests).
- Klasse `TaskItem` existiert mit `Id`, `Title`, `IsCompleted`, optional `DueDate`.
- Titel ist Pflicht und wird getrimmt; leere/Whitespace‑Titel sind nicht erlaubt.
- Erste Unit‑Tests für `TaskItem` (Defaults, Trim, Exception bei leerem Titel) grün.

**TAK (technisch):**
- Model: `TaskManager.Model.TaskItem` wie in `TaskManager.Tests.TaskItemTests` gefordert.
- Business/Persistence/UI: Nur Grundgerüst vorhanden, keine weitergehende Funktionalität erforderlich.
- Build: Solution baut unter .NET 9; UI startet (leeres Hauptfenster genügt).

---

## Iteration 2 – Aufgaben erfassen (noch nicht implementiert)
**Story:**  Als Nutzer möchte ich eine Aufgabe mit Titel hinzufügen, damit ich meine Arbeit dokumentieren kann.

**AK (fachlich):**
- Eingabefeld + Button „Add“ fügt Aufgabe hinzu.
- Leere Titel führen zu Fehlermeldung; kein Crash.
- Aufgabe erscheint in der Liste.

**TAK (technisch):**
- Business: In `TaskManager.Business.ITaskService` die Methode `TaskItem Add(string title, DateTime? dueDate)` implementieren (in `TaskManager.Business.TaskService`).
  - Validierung: `title` darf nicht leer/Whitespace sein; Trim anwenden; wirft ansonsten `ArgumentException`.
- UI: In `TaskManager.UI.WinForms.MainForm` Button „Add“ mit Textfeld verknüpfen; nach `Add` Liste aktualisieren und speichern.
- Tests: MSTest‑Klasse `TaskServiceTests` ergänzen/aktivieren: Add erzeugt neue `TaskItem`‑Instanz; bei leerem Titel Exception.

---

## Iteration 3 – Aufgaben löschen (Stub)
**Story:**  Als Nutzer möchte ich eine Aufgabe löschen können, damit erledigte/irrelevante Aufgaben verschwinden.

**AK (fachlich):**
- Button „Delete“ löscht die markierte Aufgabe.
- Nicht existierende Id wird sicher ignoriert.
- Liste aktualisiert sich sofort.

**TAK (technisch):**
- Business: `bool Delete(Guid id)` in `TaskService` implementieren; true bei Erfolg, sonst false.
- UI: `MainForm.btnDelete_Click` verdrahten; bei Erfolg `RefreshList()` und `Save()` aufrufen.
- Tests: `TaskServiceTests.Delete_ShouldReturnFalse_ForUnknownId` und „Delete entfernt Element“ abdecken.

---

## Iteration 4 – Erledigt/Unerledigt umschalten (Stub)
**Story:**  Als Nutzer möchte ich eine Aufgabe abhaken können, damit ich den Fortschritt sehe.

**AK (fachlich):**
- Checkbox oder Space‑Taste toggelt `IsCompleted`.
- Erledigte Aufgaben sind visuell erkennbar.
- Offene Aufgaben werden im Counter berücksichtigt.

**TAK (technisch):**
- Business: `bool ToggleComplete(Guid id)` in `TaskService` implementieren.
- UI: ListView‑Checkboxen mit Service koppeln (siehe `MainForm.listTasks_ItemCheck`).
- Tests: In `TaskServiceTests` Toggle ändert Status; offener Zähler passt sich an.

---

## Iteration 5 – Fälligkeitsdatum (Stub)
**Story:**  Als Nutzer möchte ich optional ein Fälligkeitsdatum (DueDate) eintragen, um Fristen im Blick zu behalten.

**AK (fachlich):**
- DatePicker mit Checkbox „Datum aktiv“.
- Ungültige Eingaben führen zu Meldung; kein Crash.
- Fälligkeitsdatum wird in der Liste angezeigt.

**TAK (technisch):**
- Model: `TaskItem.DueDate` bleibt `DateTime?` (bereits vorhanden).
- UI: DatePicker + Checkbox anbinden; Anzeige im ListView (z. B. „Title (Due: dd.MM.yyyy)“).
- Tests: `TaskServiceTests.Add_WithDueDate_SetsDueDate` prüfen; Anzeige kann via UI‑Logik/Presenter getestet werden, falls extrahiert.

---

## Iteration 6 – Zähler (Stub)
**Story:**  Als Nutzer möchte ich jederzeit sehen, wie viele Aufgaben offen sind, damit ich den Überblick behalte.

**AK (fachlich):**
- Statusbereich zeigt „Open: X“.
- Nur unerledigte Aufgaben zählen.

**TAK (technisch):**
- Business: `int CountOpen()` in `ITaskService`/`TaskService` implementieren.
- UI: `MainForm.UpdateCounters()` nutzt `CountOpen()` und aktualisiert Label.
- Tests: Unit‑Test für Zählung in `TaskServiceTests` (verschiedene Statuslagen).

---

## Iteration 7 – Warnstufen (Stub)
**Story:**  Als Nutzer möchte ich eine farbliche Warnung sehen, wenn zu viele Aufgaben offen sind, damit ich gewarnt werde.

**AK (fachlich):**
- Farbbalken wechselt je nach Level: Grün/Gelb/Rot.
- Schwellwerte sind definiert und konsistent.

**TAK (technisch):**
- Service: `TaskManager.Business.IWarningLevelEvaluator` und Implementierung `WarningLevelEvaluator` verwenden.
- UI: `MainForm.UpdateCounters()` ruft `Evaluate(open)` auf und setzt `PnlStatus.BackColor` entsprechend.
- Tests: `WarningLevelEvaluatorTests` für Grenzwerte (Gelb/Rot) ergänzen/aktivieren.

---

## Iteration 8 – Persistenz (JSON) (Stub)
**Story:**  Als Nutzer möchte ich, dass meine Aufgaben beim Neustart wieder da sind, damit ich nichts verliere.

**AK (fachlich):**
- Aufgaben werden in eine Datei gespeichert und beim Start geladen.
- Fehler beim Laden führen zu leerer Liste, nicht zum Absturz.

**TAK (technisch):**
- Repository: `TaskManager.Persistence.ITaskRepository` mit `Load()`/`Save(...)` verwenden; JSON‑Implementierung `TaskManager.Persistence.Json.JsonTaskRepository`.
- Dateipfad: `%APPDATA%\ZbW.TaskManager\tasks.json` (siehe `Program.cs`).
- Robustheit: Fehlende/kaputte Datei ergibt leere Liste (keine Exception nach aussen).
- Tests: `JsonTaskRepositoryTests` (Roundtrip, fehlende Datei, korruptes JSON) ergänzen/aktivieren.

---

## Iteration 9 – Usability (Stub)
**Story:**  Als Nutzer möchte ich komfortabel arbeiten können, damit mich die App nicht nervt.

**AK (fachlich):**
- Enter fügt neue Aufgabe hinzu.
- Doppelklick öffnet Bearbeiten‑Dialog (Titel + DueDate ändern).
- Sortierung: offene vor erledigten, danach nach Datum, dann alphabetisch.
- Überfällige Aufgaben sind optisch hervorgehoben.

**TAK (technisch):**
- UI: Key‑Handling für Enter; Doppelklick‑Handler zeigt `EditDialog`.
- Darstellung: Sortierreihenfolge und Hervorhebung überfälliger Einträge (z. B. Farbe/Font).
- Tests: Presenter/Service‑Logik testen (UI‑freie Teile); visuelle Aspekte über indirekte Kriterien prüfen, falls möglich.

---

## Iteration 10 – Fehlerbehandlung & Stabilität (Stub)
**Story:**  Als Nutzer möchte ich auch bei falschen Eingaben oder gesperrter Datei weiterarbeiten können, damit die App stabil bleibt.

**AK (fachlich):**
- Freundliche Meldungen statt Abstürzen.
- Gesperrte Datei verhindert Speichern, ohne die App zu beenden.
- Kaputte JSON wird erkannt; Start mit leerer Liste ist möglich.

**TAK (technisch):**
- UI: Zentrales Error‑Handling (try/catch) an sinnvollen Stellen; Meldungsdialoge.
- Persistence: Exceptions beim Speichern/Laden abfangen und Benutzer informieren; Lade‑Fehler münden in leere Liste.
- Tests: Fehlerpfade (z. B. gesperrte Datei simulierbar) in Unit‑/Integrationstests prüfen.

---

## Iteration 11 – Abschluss & Präsentation (Stub)
**Story:**  Als Dozierender möchte ich eine saubere, dokumentierte App sehen, damit ich die Leistung bewerten kann.

**AK (fachlich):**
- Alle öffentlichen Typen besitzen XML‑Kommentare.
- README mit kurzer Anleitung ist aktuell.
- Unit‑Tests laufen grün; App demonstriert Kernfunktionen.
- Changelog oder Story‑Nachweis ist vorhanden.

**TAK (technisch):**
- Doku: XML‑Kommentare ergänzen/vervollständigen.
- README: Build/Run/Test aktualisieren; Hinweis zu Datenpfad.
- Tests: Suite vervollständigen; Code‑Coverage optional erzeugen.

---

### Hinweis zur Template‑Solution
In diesem Template sind alle Iterationen als Stories definiert. Die Anwendung bleibt jederzeit lauffähig. Nicht implementierte Funktionen werfen eine NotImplementedException mit Hinweis, in welcher Iteration sie zu lösen sind. Die zugehörigen Unit‑Tests sind enthalten und geben bis zur Umsetzung Assert.Inconclusive aus; ersetzen Sie diese Inconclusive‑Hinweise durch echte Arrange/Act/Assert‑Schritte, sobald Sie die jeweilige Iteration implementieren.
