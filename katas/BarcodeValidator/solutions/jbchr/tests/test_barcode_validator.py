from src.barcode_validator import * 
import unittest
class TestValidate(unittest.TestCase):
    def test_ean_8(self):
        self.assertEqual(validate("40170725"), True)
        self.assertEqual(validate("28170280"), True)

    def test_ean_8_bad_checksum(self):
        self.assertEqual(validate("40170726"), False)

    def test_gtin(self):
        self.assertEqual(validate("4912345678904"), True)

    def test_gtin_bad_checksum(self):
        self.assertEqual(validate("4912345678901"), False)

    def test_bad_cases(self):
        self.assertEqual(validate("1"), False)
        self.assertEqual(validate(""), False)
        self.assertEqual(validate("AAAAAAAA"), False)
        self.assertEqual(validate("AAAAAAAACBDF"), False)

if __name__ == '__main__':
    unittest.main()