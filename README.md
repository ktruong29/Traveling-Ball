# Traveling Ball
---
The purpose of this program is to design a simple animated object moving along
a specified path using two clocks in one user interface; one clock controls
the refresh rate of the entire UI and another clock controls the frequency of
updating the coordinates of the ball.

## Specifications
---
* The initial position of the ball is in the upper right corner of the path and
the ball travels counterclockwise.
* In the lower part of the UI contains all the controls, such as buttons and
output text boxes.
* The speed of the ball is fixed in the software.
* The graphic area is refreshed at a fixed rate of 30Hz.
* There is a button with the initial label "Go". When the user clicks on "Go",
the animation starts; the ball moves counterclockwise where its center is maintained
on the path line. When the ball is in motion, the label on the "Go" button changes
to "Pause". If the user clicks on the "Pause" button, then the clock stops and the
label changes to "Go".
* When the ball is moving, the user can change the color if it by clicking
four radio buttons: green, yellow, black, and red.
* When the ball completes a circuit, then it stops moving and changes to a pink color.
* If the user clicks on "Reset" button, the clock stops and the ball returns to
its initial position.
* There are four output fields: one for the x-coordinate of the center of the ball,
one for the y-coordinate of the center of the ball, and one for the direction of
the moving ball.
* When the ball changes its direction, the output field for the direction should
specify the current path of the moving ball. There are four possible directions:
left, right, up, and down.
* When animation is in progress, these three fields are being updated every time
the graphic area refreshes.
* If the user clicks on the "Exit" button, the clock stops and the program
terminates.

## Prerequisites
---
* A virtual machine
* Install mcs

## Instruction on how to run the program
---
1. chmod +x build.sh then ./build.sh
2. sh build.sh

Copyright [2019] [Kien Truong]
