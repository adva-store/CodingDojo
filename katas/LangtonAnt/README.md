# Die Langton Ameise

Die Ameise ist eine Turingmaschine mit einem zweidimensionalen Speicher und wurde 1986 von Christopher Langton
entwickelt.  
Sie ist ein Beispiel dafür, dass ein deterministisches System aus einfachen Regeln sowohl für den Menschen visuell
überraschend ungeordnet erscheinende als auch regelmäßig erscheinende Zustände annehmen kann.  
(Wikipedia)

## Das Prinzip

Die Ameise befindet sich in einem Raster, bestehend aus quadratischen Feldern, die entweder schwarz oder weiß sein können.
In der Ausgangssituation sind alle Felder weiß und die Ameise schaut in eine bestimmte Richtung. Der Übergang zum nächsten Zustand erfolgt nach folgenden Regeln:

1.	Auf einem weißen Feld drehe 90 Grad nach rechts; auf einem schwarzen Feld drehe 90 Grad nach links.
2.	Wechsle die Farbe des Feldes (weiß nach schwarz oder schwarz nach weiß).
3.	Schreite ein Feld in der aktuellen Blickrichtung fort.

## Die Aufgabe

Programmiere die Langton-Ameise.

<img src="assets/LangtonsAntAnimated.gif"/>

### Benötigt werden zwei Lösungen.

* Die erste Lösung ist das Backend und führt den eigentlichen Algorithmus aus.
* Die zweite Lösung nimmt das Ergebnis der ersten Lösung und stellt es graphisch dar.
* Die beiden Lösungen dürfen entkoppelt von einander funktionieren.

### Anforderungen Backend

Eingabe:
* die Kantenlänge des Feldes (das Spielfeld ist immer quadratisch)
* die Startposition der Ameise
* die Blickrichtung der Ameise (n, o, s, w)
* die Anzahl der Züge

Ausgabe:
* nach jedem Zug wird das gesamte Spielfeld in einem String (kommasepariert) zurückgegeben.
* dabei wird jedes Feld mit seiner aktuellen Farbe repräsentiert (s oder w). 
* der Position der Ameise auf dem Spielfeld wird zusätzlich die jeweiligen Blickrichtung vorangestellt (n, o, s, w).
* Beispiel: (w,w,w,s,ow,s,s,w,w, …).
* die Ausgabe kann auch komplett in einer Datei gespeichert werden.

### Anforderungen Frontend

Eingabe:
* die Ausgabe des Backend (z.B. als Datei)
* die Zuggeschwindigkeit

Ausgabe:
* die Kantenlänge des Feldes
* die Startposition der Ameise
* die Blickrichtung der Ameise (n, o, s, w)
* die Anzahl der Züge

