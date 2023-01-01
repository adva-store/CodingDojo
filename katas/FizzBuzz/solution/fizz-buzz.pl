#!/usr/bin/swipl -s -q

% fizz-buzz predicates
fizzBuzzRecursion(X, fizz) :- X mod 3 =:= 0, X mod 5 =\= 0.
fizzBuzzRecursion(X, buzz) :- X mod 3 =\= 0, X mod 5 =:= 0.
fizzBuzzRecursion(X, fizzbuzz) :- X mod 3 =:= 0, X mod 5 =:= 0.
fizzBuzzRecursion(X, X) :- X mod 3 =\= 0, X mod 5 =\= 0.

% checking argument and ask recursivly 
fizzBuzz(X) :- X =< 0, 
    write('Invalid Input'), nl. 
fizzBuzz(1) :- fizzBuzzRecursion(1, Res), write(Res), nl.
fizzBuzz(X) :- X > 0,
    fizzBuzzRecursion(X,Res), write(Res), write(', '),
    XPrev is X - 1,
    fizzBuzz(XPrev).
