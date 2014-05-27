-- Challenge 164E
-- The 5 tasks are:
-- 		Output 'Hello World' to the console.
-- 		Return an array of the first 100 numbers that are divisible by 3 and 5.
-- 		Create a program that verifies if a word is an anagram of another word.
-- 		Create a program that removes a specified letter from a word.
-- 		Sum all the elements of an array
-- 	All output will be the expected output of these processes which can be verified in your normal programming language.
-- Bonus:
-- 		Implement a bubble-sort.

import Data.List

task1 = "Hello World"

task2 = map(*15)[1..100]

task3 x y = sort x == sort y

task4 x y = filter(\z -> z /= x)y

task5 = sum
	