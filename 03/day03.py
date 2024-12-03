# read input file
import sys
import re

input = 'input.txt'

def parseInput(path: str) -> str:
    lines: str = ""
    with open(path, "r", encoding="utf-8") as file:
        for line in file:
            line = line.strip()
            lines += line

    return lines


def calculateValidMemory(memory: str, regex: str) -> int:
    count = 0
    validMem = re.findall(regex, memory, re.MULTILINE)
    for mem in validMem:
        nums = [int(n) for n in mem.split(',')]
        count += (nums[0] * nums[1])
    return count


def part1():
    corruptedMem = parseInput(input)
    # matches all valid multiplication 
    regex = r'mul\((\d{1,3},\d{1,3})\)'
    print(calculateValidMemory(corruptedMem, regex))

part1()