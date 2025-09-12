# TaskManager – Projektdokumentation

Diese Dokumentation beschreibt Aufbau, Funktionsweise und Erweiterbarkeit des ZbW.TaskManager. Sie basiert auf den XML‑Kommentaren im Quellcode und ergänzt sie zu einer zusammenhängenden, leicht verständlichen Beschreibung.

Inhalt:
- Überblick
- Architektur & Schichten
- Domänenmodell
- Geschäftslogik (Business)
- Persistenz (JSON)
- Benutzeroberfläche (WinForms)
- Warnstufen (Ampellogik)
- Datenablage & Pfade
- Tests (Überblick)
- Build & Ausführen
- Erweiterbarkeit

---

## Überblick
Der TaskManager ist eine einfache Aufgabenverwaltung mit:
- WinForms‑UI zum Anlegen, Bearbeiten, Markieren und Löschen von Aufgaben,
- klarer Geschäftslogikschicht für Operations auf Aufgaben,
- JSON‑Datei als Persistenzschicht,
- Einheitstests für Kernkomponenten.

Ziel ist es, eine übersichtliche Referenzimplementierung mit sauberer Trennung von Verantwortlichkeiten bereitzustellen.

## Architektur & Schichten
Die Lösung ist in mehrere Projekte unterteilt (Schichtenarchitektur):

- Model (TaskManager.Model)
  - Enthält reine Domänenklassen (POCOs), z. B. TaskItem und das Enum WarningLevel.
- Business (TaskManager.Business)
  - Definiert Interfaces (ITaskService, IWarningLevelEvaluator) und deren Implementierungen (TaskService, WarningLevelEvaluator).
  - Kapselt Geschäftsregeln wie Validierung, Zählen offener Aufgaben oder Umschalten des Erledigt‑Status.
- Persistence (TaskManager.Persistence.Json)
  - Liefert die Schnittstelle ITaskRepository und eine JSON‑basierte Implementierung JsonTaskRepository.
  - Verantwortlich für Laden/Speichern der Aufgabenliste.
- UI (TaskManager.UI.WinForms)
  - Stellt das Hauptfenster (MainForm) und den Bearbeitungsdialog (EditDialog) bereit.
  - Bedient sich ausschließlich der Business‑Schnittstellen.
- Tests (TaskManager.Tests)
  - Enthält Komponententests für Model, Business und Persistence.

Die Composition Root befindet sich in der WinForms‑Anwendung (Program.cs), wo die konkreten Implementierungen instanziiert und verdrahtet werden.

## Domänenmodell (TaskManager.Model)

### TaskItem
Repräsentiert eine einzelne Aufgabe mit Titel, optionalem Fälligkeitsdatum und Erledigt‑Status.
- Id: Eindeutige Kennung (Guid), wird im Konstruktor gesetzt.
- Title: Darf nicht leer sein; führende/trailing Leerzeichen werden entfernt. Ungültige Titel führen zu ArgumentException.
- DueDate: Optionales Fälligkeitsdatum (DateTime?).
- IsCompleted: Boolescher Status, ob die Aufgabe erledigt ist.

Konstruktor:
- TaskItem(string title): validiert den Titel, setzt Id und IsCompleted=false.

### WarningLevel
Enum zur Anzeige eines Ampelstatus basierend auf der Anzahl offener Aufgaben:
- Green: Alles im grünen Bereich.
- Yellow: Mittlere Auslastung – Aufmerksamkeit empfohlen.
- Red: Hohe Auslastung – Handlungsbedarf.

## Geschäftslogik (TaskManager.Business)

### ITaskService / TaskService
- Add(string title, DateTime? dueDate): Fügt eine Aufgabe hinzu. Leerer/Whitespace‑Titel sind unzulässig (ArgumentException). Titel wird getrimmt.
- GetAll(): Gibt eine schreibgeschützte Liste aller Aufgaben zurück.
- CountOpen(): Zählt alle Aufgaben mit IsCompleted=false.
- ToggleComplete(Guid id): Wechselt den Erledigt‑Status einer Aufgabe. Liefert false, wenn die Aufgabe nicht gefunden wurde.
- Delete(Guid id): Entfernt die Aufgabe mit entsprechender Id. Liefert true bei Erfolg.
- Save(): Persistiert den aktuellen Stand über das Repository.
- Reload(): Lädt den Zustand neu aus dem Repository (ersetzt die In‑Memory‑Liste).

Konstruktion:
- TaskService(ITaskRepository repository): lädt initial die Aufgabenliste aus dem Repository. Das Repository ist Pflicht (ArgumentNullException bei null).

### IWarningLevelEvaluator / WarningLevelEvaluator
Bewertet den Warnstatus (Ampel) anhand der Anzahl offener Aufgaben mit konfigurierbaren Schwellwerten.
- Konstruktor: WarningLevelEvaluator(int yellowThreshold = 5, int redThreshold = 10)
  - Validiert: Beide Schwellwerte > 0 und redThreshold > yellowThreshold, sonst ArgumentException.
- Evaluate(int count):
  - count >= redThreshold -> Red
  - sonst count >= yellowThreshold -> Yellow
  - sonst Green

## Persistenz (TaskManager.Persistence.Json)

### ITaskRepository
Abstrakte Schnittstelle für Persistenzoperationen:
- Load(): Lädt alle Aufgaben.
- Save(IEnumerable<TaskItem> items): Speichert alle Aufgaben.

### JsonTaskRepository
JSON‑basierte Implementierung der Persistenz:
- Konstruktor: JsonTaskRepository(string filePath)
  - Validiert, dass filePath nicht leer/Whitespace ist.
- Load():
  - Liest die JSON‑Datei, wenn vorhanden. Bei Fehlern oder nicht vorhandener Datei wird eine leere Liste zurückgegeben (robustes Verhalten).
  - Deserialisiert in List<TaskItem> (PropertyNameCaseInsensitive=true).
  - Filtert ungültige Einträge (null oder leerer Titel) heraus.
- Save(items):
  - Validiert items != null, sonst ArgumentNullException.
  - Serialisiert mit Einrückung (WriteIndented=true) und schreibt die Datei.

## Benutzeroberfläche (TaskManager.UI.WinForms)

### Program (Composition Root)
- Ermittelt den Datenordner: %APPDATA%\ZbW.TaskManager.
- Erstellt den Ordner bei Bedarf und setzt den Dateipfad auf tasks.json.
- Instanziiert JsonTaskRepository, WarningLevelEvaluator (Standard: Gelb=5, Rot=10) und TaskService.
- Startet MainForm mit diesen Abhängigkeiten.

### MainForm
Hauptfenster der Anwendung zum Anzeigen und Verwalten von Aufgaben.
- Add: Fügt anhand der Eingaben (Titel, optional Fälligkeitsdatum) eine neue Aufgabe hinzu; speichert direkt danach.
- Delete: Löscht die aktuell ausgewählte Aufgabe; speichert bei Erfolg.
- Toggle: Wechselt den Erledigt‑Status der Auswahl; speichert bei Erfolg.
- Doppelklick auf ListView‑Eintrag: Öffnet EditDialog; nach Bestätigung Aktualisierung und Speichern.
- Checkbox‑Änderung in der Liste: Synchronisiert IsCompleted sofort mit dem Service (ItemCheck‑Handler) und aktualisiert Zähler/Status.
- RefreshList(): Bindet Service‑Daten an die ListView und aktualisiert Zähler.
- UpdateCounters(): Zeigt die Anzahl offener Aufgaben an und setzt die Hintergrundfarbe eines Status‑Panels gemäß WarningLevel (Grün/Khaki/IndianRed).

### EditDialog
Dialog zum Bearbeiten eines TaskItem:
- Felder: Titel, optionales Fälligkeitsdatum, erledigt.
- Apply(): Validiert den Titel (nicht leer), überträgt Werte in das TaskItem; bei ungültigen Eingaben bleibt der Dialog geöffnet.

## Warnstufen (Ampellogik)
Die MainForm lässt über IWarningLevelEvaluator den Ampelstatus basierend auf der Anzahl offener Aufgaben berechnen und färbt ein Panel entsprechend:
- Green -> LightGreen
- Yellow -> Khaki
- Red -> IndianRed

Standard‑Schwellen: Gelb ab 5 offenen Aufgaben, Rot ab 10.

## Datenablage & Pfade
- Datei: %APPDATA%\ZbW.TaskManager\tasks.json
- Format: JSON‑Liste von TaskItem‑Objekten (Id, Title, IsCompleted, DueDate)
- Verhalten: Beim Laden wird eine nicht vorhandene oder fehlerhafte Datei toleriert (es wird eine leere Liste geliefert). Beim Speichern wird mit Einrückung geschrieben.

## Tests (Überblick)
Das Projekt enthält Komponententests (MSTest):
- JsonTaskRepositoryTests: Verhalten bei fehlender/inkonsistenter Datei, Laden/Speichern.
- TaskServiceTests: Add, ToggleComplete, Delete, Reload, DueDate‑Handling und Validierungsfälle.
- TaskItemTests: Titel‑Validierung, Trimmen, Defaults.
- WarningLevelEvaluatorTests: Grenzwerte, ungültige Konstruktorparameter.

Hinweis: In der gelieferten Musterlösung laufen alle Tests grün (Stand laut Source/README.md).

## Build & Ausführen
Voraussetzungen:
- Windows, .NET 9 SDK

Build:
- dotnet build (oder IDE)

Tests:
- dotnet test (oder IDE Test Runner)

Start der Anwendung:
- Projekt TaskManager.UI.WinForms ausführen. Die Aufgaben werden unter %APPDATA%\ZbW.TaskManager\tasks.json gespeichert.

## Erweiterbarkeit (Auswahl)
- Dependency Injection: Auslagerung der Objektkonstruktion aus Program.cs in einen DI‑Container.
- Persistenzhärtung: Atomare Schreibvorgänge (z. B. über temporäre Datei und File.Replace), Logging/Fehlerdialoge.
- UI‑Features: Markieren überfälliger Aufgaben, Sortierung/Filterung, Detailansicht mit Spalten.
- Konfiguration: Schwellenwerte und Pfade aus appsettings.json laden.
- Weitere Repositories: z. B. In‑Memory, CSV, SQLite.

—
Stand: 2025‑09‑10