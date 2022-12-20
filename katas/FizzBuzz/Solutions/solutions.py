# Fizzbuzz solution
# Code by Daiden Sacha

def fizzBuzz():
    # Assign the values 1-100 to a list
    values = range(1,100)

    # Loop through the list
    for i in values:
        # If the number is divisible by 3 and 5 then print FizzBuzz
        if i % 3 == 0 and i % 5 == 0:
            print("fizzBuzz")
        # If number is divisible by 3 then print Fizz
        elif i % 3 == 0:
            print("Fizz")
        # If number is divisible by 5 then print Buzz
        elif i % 5 == 0:
            print("Buzz")

fizzBuzz();