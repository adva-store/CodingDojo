# Brainfuck

Brainfuck ist eine esoterische Programmiersprache, die der Schweizer Urban Müller im Jahre 1993 entwarf.

Brainfuck ist gut geeignet, um Grundlagen der Computertechnik zu erlernen. Speziell zeichnet sich Brainfuck durch ein
extrem einfaches Sprachkonzept und hochkompakte Realisierung des Compilers aus, gleichzeitig wurde aber die universelle
Einsetzbarkeit nicht eingeschränkt.  

Ein Brainfuck-Programm ähnelt stark der formalen Definition einer Turingmaschine. Statt eines Lese-/Schreibkopfes auf einem Band, Zuständen, sowie einem frei definierbaren Alphabet werden jedoch im Sinne einer iterativen Programmiersprache ein Zeiger auf ein Datenfeld, Schleifenkonstrukte und eine rudimentäre ALU verwendet.  

(siehe Wikipedia)

## Befehlssatz

Brainfuck besitzt acht Befehle, jeweils bestehend aus einem einzigen Zeichen:

| Zeichen   | C-Äquivalent              | Semantik |
|-----------|---------------------------|------------|
|>	        | ptr++;	                |inkrementiert den Zeiger|
| <         | ptr--;	                |dekrementiert den Zeiger|
| +         | cell[ptr]++;              |inkrementiert den aktuellen Zellenwert|
| −         | cell[ptr]--;              |dekrementiert den aktuellen Zellenwert|
| .         | putchar (cell[ptr]);      |Gibt den aktuellen Zellenwert als ASCII-Zeichen auf der Standardausgabe aus|
| ,         | cell[ptr] = getchar();    |Liest ein Zeichen von der Standardeingabe und speichert dessen ASCII-Wert in der aktuellen Zelle|
| [         | while (cell[ptr]) {	    |Springt nach vorne, hinter den passenden ]-Befehl, wenn der aktuelle Zellenwert 0 ist|
| ]         | }	                        |Springt zurück, hinter den passenden [-Befehl, wenn der aktuelle Zellenwert nicht 0 ist		|

Alle anderen Zeichen werden ignoriert und können im Programmcode als Kommentar dienen.

## _Hello World!_ als Beispiel

```
++++++++++
 [
  >+++++++>++++++++++>+++>+<<<<-
 ]                       Schleife zur Vorbereitung der Textausgabe
 >++.                    Ausgabe von 'H'
 >+.                     Ausgabe von 'e'
 +++++++.                'l'
 .                       'l'
 +++.                    'o'
 >++.                    Leerzeichen
 <<+++++++++++++++.      'W'
 >.                      'o'
 +++.                    'r'
 ------.                 'l'
 --------.               'd'
 >+.                     '!'
 >.                      Zeilenvorschub
 +++.                    Wagenrücklauf
```

## Die Aufgabe

Schreib einen Brainfuck-Interpreter, der beliebige BrainFuck-Programme ausführen kann.