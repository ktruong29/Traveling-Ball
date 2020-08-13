#!/bin/bash
# /*******************************************************************************
#  *Author: Kien Truong
#  *Date: Oct 14, 2019
#  ******************************************************************************/

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile TravelingBall.cs to create the file: TravelingBall.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:TravelingBall.dll TravelingBall.cs

echo Link the previously created dll file to create an executable file.
mcs -r:System -r:System.Windows.Forms -r:TravelingBall.dll -out:Traveling.exe main.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 3 program.
./Traveling.exe

echo The script has terminated.
