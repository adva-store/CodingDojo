package main;

public class Palindrome {

    public boolean isPalindrome(String s) {
        if(s == null)
            return false;
        int start = 0;
        int end = (s.length() - 1);
        while ((start < end)) {

            char first = s.charAt(start);
            char second = s.charAt(end);
            if(!Character.isLetterOrDigit(first)){
                start++;
                continue;
            }

            if(!Character.isLetterOrDigit(second)) {
                end--;
                continue;
            }

            if (Character.toLowerCase(first) != Character.toLowerCase(second)) {
                return false;
            }

            start++;
            end--;
        }

        return true;
    }
}
