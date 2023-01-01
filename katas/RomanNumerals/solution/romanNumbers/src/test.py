import unittest

from romanionNumber import decimal_to_roman, roman_to_decimal

class TestRomanionNumber(unittest.TestCase):
    
    def test_inverse_roman_to_decimal(self):
        """
        if a numnber gets first converted to a roman numral and than back it shoudl equal the original number
        """

        for n in range(3001):
            self.assertEqual(roman_to_decimal(decimal_to_roman(n)), n)


    def test_decimal_to_roman_additive(self):
        self.assertEqual(decimal_to_roman(3), 'III')
        self.assertEqual(decimal_to_roman(11), 'XI')
        self.assertEqual(decimal_to_roman(578), 'DLXXVIII')


    def test_decimal_to_roman_subtract(self):
        self.assertEqual(decimal_to_roman(4), 'IV')
        self.assertEqual(decimal_to_roman(49), 'XLIX')
        self.assertEqual(decimal_to_roman(491), 'CDXCI')

    
    def test_decimal_to_roman_edge_cases(self):
        self.assertEqual(decimal_to_roman(1), 'I')
        self.assertEqual(decimal_to_roman(3000), 'MMM')


    def test_roman_to_decimal(self):
        self.assertEqual(roman_to_decimal('IX'), 9)
        self.assertEqual(roman_to_decimal('XCIV'), 94)
        self.assertEqual(roman_to_decimal('CDXI'), 411)
        self.assertEqual(roman_to_decimal('III'), 3)
        self.assertEqual(roman_to_decimal('DLXXVIII'), 578)

    def test_roman_to_decimal_edge_cases(self):
        self.assertEqual(roman_to_decimal('I'),  1)      
        self.assertEqual(roman_to_decimal('MMM'), 3000)

        

if __name__ == '__main__':
    unittest.main()
    