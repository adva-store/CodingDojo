# Strange Chessboard solution supposed by Kai Bielefeld | texxo

## setup
- plain php implementation - php was already installed
- phpunit to test the service
  - `wget -O phpunit.phar https://phar.phpunit.de/phpunit-10.phar`
  - `chmod +x phpunit.phar`

## implementation
- implemented Chessboard Model
  - represents the chessboard with specific attributes
- implemented phpunit test
- implemented CalculationService
  - calculates general chessboard size and size of only black or white fields
- implemented main.php
  - runs the script that creates a chessboard and calls the calculationService to calculate the black-, white- and general field size of the chessboard
