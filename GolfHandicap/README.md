Using a mix of Vertical Slice and Clean Code Architecture.  VSA has too many files, and felt like I could reuse some of them instead of having the bare mininum in them.


# Course Rating and slope
Senaca doesn't post their 9 hole ratings, so here is how it is calculated
slope is the same, but it's not perfect since it isn't linear
SLOPE IS ALWAYS THE FULL AMOUNT, IT'S RELATIVE

F = rating of Furnance
C = rating of Chippewa
B = rating of Baldwin

### Blues

F+C = 71.7
C+B = 73.0
F+B = 73.5

C = ((71.7+73.7)-73.5) / 2 = 35.6
B = ((73.5+73.0)-71.7) / 2 = 37.4
F = ((73.5+71.7)-73.0) / 2 = 36.1

Slope 

C = 61.5 * 2 = 123
B = 63.5 * 2 = 127
F = 63.5 8 2 = 127

### Greens

F+C = 69.9
C+B = 70.8
F+B = 71.1

C = ((69.9+70.8)-71.1) / 2 = 34.8
B = ((70.8+71.1)-69.9) / 2 = 36.0
F = ((69.9+71.1)-70.8) / 2 = 35.1

Slope

C = ((120+121)-124) / 2 = 58.4 * 2 = 116
B = ((121+124)-120) / 2 = 62.5 * 2 = 125
F = ((120+124)-121) / 2 = 61.5 * 2 = 123

### Reds

F+C = 67.8
C+B = 67.3
F+B = 68.1

C = ((67.8+67.3)-68.1) / 2 = 33.5
B = ((68.1+67.3)-67.8) / 2 = 33.8
F = ((67.8+68.1)-67.3) / 2 = 34.3

Slope
F+C = 113
C+B = 113
F+B = 116

C = ((113+113) - 116) / 2 = 55 * 2 = 110 
B = ((113+116) - 113) / 2 = 58 * 2 = 116
F = ((113+116) - 113) / 2 = 58 * 2 = 116

# Handicap Calculation

This is not the same as USGA rules
I will be tracking the past 6 rounds at 80%
At the beginning of the year, the past 10 of 20 rounds at 80% will be used to determine a golfer's handicap
Adjusted Gross Score will be double par (USGA uses double bogey)

This is how the handicap index will be calc
From the past 6 scores
Handicap Differential = ((Adjusted Gross Score - Course Rating) * 113) / Slope Rating
Then average it, then * 80%

Then we'll get the Handicap Index

Handicap Index = (Sum of past 6 rounds) / 6 * .8(80%)

### Start of Year

Need to grab the past 20 scores
Find the diff
take the lowest 10
then get the average * 80%
Handicap Index = ((Sum of past 10 low rounds of 20) / 10) * .8(80%)