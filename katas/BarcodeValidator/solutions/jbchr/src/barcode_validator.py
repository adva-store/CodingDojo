import math
import sys

def validate(code):
    # Cases: no numbers, not length 8 or 12
    if not code.isnumeric() or not (len(code) != 8 or len(code) != 12):
        return False

    # build product checksum
    checksum_product = 0
    for i in range(0, len(code) - 1):
        multiplier = 3 if i % 2 == 0 else 1
        checksum_product = checksum_product + multiplier * int(code[i])

    # build checksum
    checksum = math.ceil(checksum_product / 10) * 10 - checksum_product

    #return result
    return checksum == int(code[len(code) - 1])


# CLI
print(validate(sys.argv[1]))

