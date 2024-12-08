input = 'input.txt'

def parseInput(path: str) -> list[str]:
    matrix = []
    with open(path, "r", encoding="utf-8") as file:
        for line in file:
            matrix.append(line.rstrip('\n'))
    return matrix

# PART 1
def countXMAS(words: list[str]) -> int:
    count: int = 0
    for x, line in enumerate(words[3:len(words)-3], start=3):
        for y, char in enumerate(line[3:len(line)-3], start=3):
            if char == 'X':
                # easiest case: we have inline xmas
                if line[y:y+4] == "XMAS":
                    count += 1
                if line[y-3:y+1] == 'SAMX':
                    count += 1
                if ''.join([char, words[x-1][y], words[x-2][y], words[x-3][y]]) == "XMAS": # check vertically upwards
                    count += 1
                if ''.join([char, words[x+1][y], words[x+2][y], words[x+3][y]]) == "XMAS": # check vertically downwards
                    count += 1
                if ''.join([char, words[x-1][y-1], words[x-2][y-2], words[x-3][y-3]]) == "XMAS": # check diagonal up left
                    count += 1
                if ''.join([char, words[x+1][y-1], words[x+2][y-2], words[x+3][y-3]]) == "XMAS": # check diagonal up right
                    count += 1
                if ''.join([char, words[x-1][y+1], words[x-2][y+2], words[x-3][y+3]]) == "XMAS": # check diagonal down left
                    count += 1
                if ''.join([char, words[x+1][y+1], words[x+2][y+2], words[x+3][y+3]]) == "XMAS": # check diagonal down right
                    count += 1
    return count

# PART 2
def countMAS(words: list[str]) -> int:
    count: int = 0
    for x, line in enumerate(words[2:len(words)-2], start=2):
        for y, char in enumerate(line[2:len(line)-2], start=2):
            if char == 'A':
                if  (( (words[x-1][y-1] == 'M' and  words[x+1][y+1] == 'S') or
                        (words[x-1][y-1] == 'S' and  words[x+1][y+1] == 'M')) and # diagonal top left to bottom right
                        ((words[x-1][y+1] == 'S' and  words[x+1][y-1] == 'M') or 
                        (words[x-1][y+1] == 'M' and  words[x+1][y-1] == 'S'))): # diagonal top right to bottom left
                    count += 1
    return count

def padWords(words: list[str]) -> list[str]:
    pad = "." * (len(words[0]) + 6)
    padList = []
    for _ in range(3):
        padList.append(pad)
    # Add padding to each line
    paddedWords = []
    for word in words:
        paddedWord = "..." + word + "..."
        paddedWords.append(paddedWord)
    padList.extend(paddedWords)
    # add padding to bottom of grid
    for _ in range (3):
        padList.append(pad)
    return padList

paddedList = padWords(parseInput(input))
print(countXMAS(paddedList))
print(countMAS(paddedList))
