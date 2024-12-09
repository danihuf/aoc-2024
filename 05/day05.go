package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	// parse input
	content, err := os.ReadFile("./input.txt")
	if err != nil {
		fmt.Print(err)
	}
	split := strings.Split(string(content), "\n\n")
	rules := strings.Split(split[0], "\n")
	updatesStr := strings.Split(split[1], "\n")
	count := getSortedUpdates(rules, updatesStr)
	print(count) // print part 1
}

// PART 1
func getSortedUpdates(rules []string, updates []string) int {
	countMid := 0
	// check for each update if it is sorted according to the rules
	for _, update := range updates {
		isUpdateSorted := false
		for _, rule := range rules {
			conditions := strings.Split(rule, "|")
			first := conditions[0]
			second := conditions[1]
			if strings.Contains(update, first) && strings.Contains(update, second) {
				if strings.Index(update, first) < strings.Index(update, second) {
					isUpdateSorted = true
				} else {
					isUpdateSorted = false
					break
				}
			}
		}
		if isUpdateSorted {
			updateSlice := strings.Split(update, ",")
			midStr := updateSlice[len(updateSlice)/2]
			mid, err := strconv.Atoi(midStr)
			if err != nil {
				fmt.Print(err)
				continue
			}
			countMid += mid
		}
	}
	return countMid
}
