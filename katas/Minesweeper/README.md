# Mindsweeper

[Minesweeper](https://de.wikipedia.org/wiki/Minesweeper) ist ein simples, dem Betriebssystem Microsoft Windows bis einschließlich der Version Windows 7 beigelegtes Computerspiel, bei dem Spieler durch eine Kombination aus logischem Denken und (bisweilen) zufälligem Raten herausfinden sollen, unter welchen Feldern Minen versteckt sind. Das Ziel ist, alle Felder aufzudecken, hinter welchen keine Minen verborgen sind. Aufgedeckte Felder werden mit Ziffern markiert, die angeben, wie viele Minen an das Feld angrenzen. Als zusätzliche Herausforderung läuft eine Stoppuhr, sodass das Spiel zumeist auf Zeit gespielt wird.

# Aufgabe

Schreibe ein Programm, das zu einem Minesweeper Spielfeld einen Mogelzettel erstellt.

Aufgabe des zu erstellenden Programms „Mogelzettel“ ist es, einen Mogelzettel zu erstellen, aus dem die Anzahl der angrenzenden Minen (* ist das Symbol für eine Mine) zu jedem Feld ersichtlich ist.

Eine Eingabedatei für das Programm sieht wie folgt aus:
```
*...
.... 
.*.. 
....
```

Das Ergebnis ist eine Ausgabedatei mit folgendem Inhalt:

```
*100
2210
1*10
1110
```

Weitere Beispiele für Eingaben und Ausgaben des Programms:

```
**...     **100
.....     33200
.*...     1*100
```

Die Spielfeldgröße kann sowohl in der Anzahl der Zeilen als auch Spalten variieren. Die Eingabedateien sind allerdings immer korrekt aufgebaut, eine Fehlerbehandlung ist nicht erforderlich.