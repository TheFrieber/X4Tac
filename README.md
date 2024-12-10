# X4Tac - 4x4 Tic-Tac-Toe  

X4Tac ist ein 4x4-Tic-Tac-Toe-Spiel, das mit C# und .NET MAUI entwickelt wurde. Es unterstützt mehrere Plattformen, darunter Android und Windows, und nutzt moderne Software-Design-Prinzipien wie MVVM (Model-View-ViewModel) und Dependency Injection.  

---

## Features  

### Gameplay  
- **4x4 Spielfeld**: Spielen Sie auf einem erweiterten Tic-Tac-Toe-Brett.  
- **Zwei-Spieler-Modus**: Zwei Spieler wechseln sich ab, um Kreuz oder Kreis zu setzen.  
- **Verlustregel**: Ein Spieler verliert, wenn er gezwungen ist, drei Symbole in einer Reihe, Spalte oder Diagonale zu setzen.  
- **Unentschieden**: Das Spiel endet unentschieden, wenn alle Felder belegt sind und keiner verloren hat.  

### Zusätzliche Funktionen  
- **Spielstand speichern und laden**: Der aktuelle Spielstand (Spielfeld, Spieler am Zug und KI-Modus) kann gespeichert und später wiederhergestellt werden.  
- **Option, gegen eine KI zu spielen**: Spielen Sie gegen eine Minimax-basierte KI, die optimale Züge berechnet.  

### Plattformübergreifende Unterstützung  
- **Responsives UI**: Passt sich automatisch an verschiedene Bildschirmgrößen und Eingabemethoden an (Touchscreen auf Android, Maus/Tastatur auf Windows).  

---

## Technische Umsetzung  

### Architektur  
- **MVVM-Design-Pattern**:  
  - **Models**: Spiellogik und Datenstrukturen (z. B. Spielfeld).  
  - **ViewModels**: Bindeglied zwischen UI und Spiellogik.  
  - **Views**: Benutzeroberfläche in XAML.  
- **Services**: Abstraktionen für wiederverwendbare Funktionen (z. B. Speicherung von Spielständen).  

### Spielmechanik  
- **Minimax-Algorithmus mit Alpha-Beta-Pruning**:  
  - Berechnet optimale Züge durch Simulation aller möglichen Spielverläufe.  
  - Reduziert Rechenaufwand durch Überspringen irrelevanter Spielzüge.  

- **Regeln und Validierung**:  
  - Überprüft automatisch, ob ein Spieler gewonnen, verloren oder ob das Spiel unentschieden ist.  
  - Überwacht Regelverstöße und aktualisiert das Spiel entsprechend.  

### Speicherfunktion  
- **Spielstand speichern**:  
  - Speichert Spielfeld, aktuellen Spieler und KI-Modus in einem plattformübergreifenden Schlüssel-Wert-Speicher (Preferences).  
- **Spielstand laden**:  
  - Lädt den Spielstand und aktualisiert das UI basierend auf den gespeicherten Daten.  

---

## Installation  

1. **Systemanforderungen**:  
   - Visual Studio 2022 mit .NET MAUI-Unterstützung.  
   - Android-Emulator oder Windows-Gerät.  

2. **Projekt einrichten**:  
   - Klonen Sie das Repository:  
     ```bash  
     git clone https://github.com/TheFrieber/X4Tac.git  
     ```  
   - Öffnen Sie das Projekt in Visual Studio.  
   - Stellen Sie sicher, dass alle NuGet-Pakete installiert sind (z. B. `CommunityToolkit.Mvvm`).  

3. **App starten**:  
   - Wählen Sie das gewünschte Ziel (z. B. Android-Emulator, Windows).  
   - Starten Sie die App mit `F5` oder `Run`.  

---

## Verwendete Technologien  

- **.NET MAUI**: Plattformübergreifende App-Entwicklung.  
- **CommunityToolkit.Mvvm**: Unterstützung für das MVVM-Muster.  
- **C#**: Kernsprache der App.  

---

## Autor  

Dieses Projekt wurde als Teil einer Bewerbungsaufgabe entwickelt.

---

## Preview

**Windows**:
![WindowsX4Tac](https://github.com/user-attachments/assets/3355aca6-1248-44c5-88e6-33974827087c)


**Android(-Emulator)**:
![WindowsX4Tac](https://github.com/user-attachments/assets/5a26aef2-f8ce-42cd-ae76-bfb730bd7f0b)



---

### Lizenz  
Dieses Projekt steht unter der MIT-Lizenz.
