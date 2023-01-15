package test;

import main.Palindrome;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

class PalindromeTests {
    @ParameterizedTest(name = "{index} => s=''{0}'', expected={1}")
    @MethodSource("testData")
    void isPalindrome(String s, boolean expected)
    {
        var palindrome = new Palindrome();
        var res = palindrome.isPalindrome(s);
        assertEquals(expected, res);
    }


    private static Stream<Arguments> testData() {
        return Stream.of(
                Arguments.of("amma", true),
                Arguments.of("A man, a plan, a canal: Panama", true),
                Arguments.of("race a car", false),
                Arguments.of("street", false),
                Arguments.of(" ", true),
                Arguments.of("Tarne nie deinen Rat!", true)
        );
    }

    @Test
    void isPalindrome_StringIsNull_ReturnFalse()
    {
        var palindrome = new Palindrome();
        String s = null;
        var res = palindrome.isPalindrome(s);
        assertEquals(false, res);
    }
}