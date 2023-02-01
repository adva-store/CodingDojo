const readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});
  
readline.question('Enter a word or phrase to check for palindrom?', name => {
    palindrome(name) ? console.log(`Your input is a Palindrom!`) : console.log(`Your input is not a Palindrom!`);
    readline.close();
});

function palindrome(str) {
    var re = /[\W_]/g;
    var lowRegStr = str.toLowerCase().replace(re, '');
    var reverseStr = lowRegStr.split('').reverse().join(''); 
    return reverseStr === lowRegStr;
}

