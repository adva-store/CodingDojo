/*
  Palindrome Checker
  ------------------
  Checks if a word or sentence is spelled the same way both forward and backward, ignoring punctuation, case, and spacing.

  Return true if the given string is a palindrome. Otherwise, return false.

  Code by: Daiden Sacha
*/

// Define regex, includes all punctuation and spaces
let punctuation = /[!"#$%&'()//+,-./:;<=>?@[\]^_`{|}~ ]/g;

// SOLUTION 1 (recursively check each char)
function isPalindrome(str) {
  // remove punctionation and spaces from string and make lowercase
  let cleanedStr = str.replace(punctuation, '').toLowerCase();
  // create array of characters, using every() to check each char
  return cleanedStr.split('').every((char, i) => {
    return char === cleanedStr[cleanedStr.length - i - 1];
  });
}

// SOLUTION 2 (reverse string and compare)
// function isPalindrome(str) {
//   // remove punctionation and spaces from string and make lowercase
//   let cleanedStr = str.replace(punctuation, '').toLowerCase();
//   // reverse string and compare to original
//   return cleanedStr === cleanedStr.split('').reverse().join('');
// }

// Test cases
isPalindrome('Abba'); // => true
isPalindrome('Lagerregal'); // => true
isPalindrome('Reliefpfeiler'); // => true
isPalindrome('Rentner'); // => true
isPalindrome('Dienstmannamtsneid'); // => true
isPalindrome('Tarne nie deinen Rat!'); // => true
isPalindrome('Eine güldne, gute Tugend: Lüge nie!'); // => true
isPalindrome(
  'Ein agiler Hit reizt sie. Geist?! Biertrunk nur treibt sie. Geist ziert ihre Liga nie!',
); // => true
