#!/usr/bin/env python3

from collections import OrderedDict
import sys

values_rom_to_dec = {
    'I': 1,
    'V': 5,
    'X': 10,
    'L': 50,
    'C': 100,
    'D': 500,
    'M': 1000
}

# order is important in dec_to_romanian
values_dec_to_rom = OrderedDict([
    [1000, 'M'], 
    [900, 'CM'], 
    [500, 'D'], 
    [400, 'CD'], 
    [100, 'C'], 
    [90, 'XC'], 
    [50, 'L'], 
    [40, 'XL'], 
    [10, 'X'],
    [9, 'IX'],
    [5, 'V'],
    [4, 'IV'], 
    [1, 'I']
])

def roman_to_decimal(r_num, count=1, total=0):
    """
    count all characters recursivly and add/subtract in the end
    """

    # needed as recursion end case, even if not allowed in the specs
    if not r_num:
        return total

    char = r_num[0]
    r_num = r_num[1:]

    try:    
        next_char = r_num[0]
    except IndexError:
        # there is no next char, so calculate the final value
        return total + count * values_rom_to_dec[char]

    if next_char == char:
        # sama character -> continue counting but dont add anything so far.
        return roman_to_decimal(r_num, count + 1, total) 

    elif values_rom_to_dec[char] > values_rom_to_dec[next_char]:
        # 'additive' case with new character. reset count and add current vale
        return roman_to_decimal(r_num, 1, total + count * values_rom_to_dec[char])
    else:
        # 'subtractive' case with new character. reset count and subtract crrent vale
        return roman_to_decimal(r_num, 1, total - count * values_rom_to_dec[char])
    

def decimal_to_roman(num):
    """
    iterate over all dec values corresponding to roman numerals and add up how as oftne as it fits
    """
    res = ''

    for i in list(values_dec_to_rom.keys()):
        tmp = num // i
        if i > num:
            continue


        res += tmp * values_dec_to_rom[i]
        num = num % i

    return res


if __name__ == '__main__':
    if not len(sys.argv) == 2:
        print("Please put as only argument an decimal number or roman numeral between 1 and 3000")
        exit(1)
    
    
    try:
        num = int(sys.argv[1])
        print(decimal_to_roman(num))
    except ValueError:
        print(roman_to_decimal(sys.argv[1]))
    
    exit(0)