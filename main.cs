/*******************************************************************************
 *Author: Kien Truong
 *Date: Oct 14, 2019
 ******************************************************************************/

using System;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class TravelingBallMain
{
  static void Main(string[] args)
  {
    TravelingBall travelingBallApp = new TravelingBall();
    Application.Run(travelingBallApp);
    System.Console.WriteLine("Main method will now shutdown.");
  }//End of Main
}//End of TravelingBallMain
