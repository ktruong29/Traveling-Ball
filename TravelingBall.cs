/*******************************************************************************
 *Author: Kien Truong
 *Date: Oct 14, 2019
 ******************************************************************************/

using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;

public class TravelingBall: Form
{
  private const int maxFormWidth  = 1920;
  private const int maxFormHeight = 1080;
  private const int minFormWidth  = 640;
  private const int minFormHeight = 360;
  Size maxFrameSize = new Size(maxFormWidth,maxFormHeight);
  Size minFrameSize = new Size(minFormWidth,minFormHeight);

  private const int topPanelHeight    = 50;
  private const int bottomPanelHeight = 110;
  private const double delta          = 12.5; //Animation speed : distance traveled during
                                              //each tic of the animation clock.
  private const double refreshClockSpeed   = 30.0; //Hz
  private const double animationClockSpeed = 45.0; //Hz
  private const double radius = 7.0;
  private const double milsecPerSec = 1000.0; //Number of milliseconds per second.
  private const double animationInterval = milsecPerSec/animationClockSpeed;
  private const double refreshInterval = milsecPerSec/refreshClockSpeed;

  private const String welcome_message = "Traveling Ball by Kien Truong";
  private System.Drawing.Font welcome_style = new System.Drawing.Font("TimesNewRoman",24,FontStyle.Regular);
  private Brush welcome_paint_brush = new SolidBrush(System.Drawing.Color.Black);
  private Point welcome_location;   //Will be initialized in the constructor.

  //Initializing three buttons
  private Button goButton    = new Button();
  private Button resetButton = new Button();
  private Button exitButton  = new Button();

  //Instanciating four radio buttons for color change
  private RadioButton greenButton   = new RadioButton();
  private RadioButton yellowButton  = new RadioButton();
  private RadioButton blackButton   = new RadioButton();
  private RadioButton redButton     = new RadioButton();
  private GroupBox radioContainer   = new GroupBox();

  private Label  directionLabel   = new Label();
  private Label  xCenterBall      = new Label();
  private Label  yCenterBall      = new Label();

  private enum OrientationDir{left,right,down,up};
  OrientationDir orientation = OrientationDir.left;
  private enum BallColor{Green,Yellow,Black,Red};
  BallColor ballC = BallColor.Red;

 /*
 Declare variables related to points on the rectangular path
 p0 = (p0x,p0y) is the starting point and the forth 90 degree turn occurs
 p1 = (p1x,p1y) is where the first 90 degree turn occurs
 p2 = (p2x,p2y) is where the second 90 degree turn occurs
 p3 = (p3x,p3y) is where the third 90 degree turn occurs
 */
 private int p0x;
 private int p0y;
 private int p1x;
 private int p1y;
 private int p2x;
 private int p2y;
 private int p3x;
 private int p3y;

 //Used to determine where to turn
 private Point startPoint;  //This will be constructed inside the interface constructor
 private Point upperLeftCornerPoint;
 private Point lowerLeftCornerPoint;
 private Point lowerRightCornerPoint;

 private int formWidth;
 private int formHeight;

 //Coordinates of the upper left corner of the ball
 private double x; //This is x-coordinate of the upper left corner of the ball.
 private double y; //This is y-coordinate of the upper left corner of the ball.

 private bool clocks_are_stopped = true;

 //Declare clocks
 private static System.Timers.Timer refreshClock    = new System.Timers.Timer();
 private static System.Timers.Timer ballUpdateClock = new System.Timers.Timer();

public TravelingBall()
{//Set the size of the user interface box.
  formWidth  = (maxFormWidth+minFormWidth)/2;
  formHeight = (maxFormHeight+minFormHeight)/2;
  Size = new Size(formWidth,formHeight);

  MaximumSize = maxFrameSize;
  MinimumSize = minFrameSize;

  //Initialize text strings
  Text = "Traveling Ball by Kien Truong";
  System.Console.WriteLine("Form_width = {0}, Form_height = {1}.", Width, Height);
  goButton.Text       = "Go";
  resetButton.Text    = "Reset";
  exitButton.Text     = "Exit";
  redButton.Text      = "Red";
  blackButton.Text    = "Black";
  yellowButton.Text   = "Yellow";
  greenButton.Text    = "Green";
  radioContainer.Text = "Color";

  //Set sizes
  goButton.Size       = new Size(100,40);
  resetButton.Size    = new Size(100,40);
  exitButton.Size     = new Size(100,40);
  directionLabel.Size = new Size(50,20);

  radioContainer.Size = new Size(300,50);
  greenButton.Size    = new Size(60,20);
  yellowButton.Size   = new Size(60,20);
  blackButton.Size    = new Size(60,20);
  redButton.Size      = new Size(60,20);

  xCenterBall.Size = new Size(70,20);
  yCenterBall.Size = new Size(70,20);

  //Set the four key points in the path of the moving ball
  //Upper right corner p0x = 1130, p0y = 100
  p0x = Width-150;
  p0y = topPanelHeight+50;
  startPoint = new Point (p0x,p0y);
  System.Console.WriteLine("p0x = {0}, p0y = {1}.", p0x, p0y);

  //Upper left corner p1x = 80, p1y = 100
  p1x = 80;
  p1y = p0y;
  upperLeftCornerPoint = new Point(p1x,p1y);
  System.Console.WriteLine("p1x = {0}, p1y = {1}.", p1x, p1y);

  //Lower left corner p2x = 80, p2y = 500
  p2x = p1x;
  p2y = 500;
  lowerLeftCornerPoint = new Point(p2x,p2y);
  System.Console.WriteLine("p2x = {0}, p2y = {1}.", p2x, p2y);

  //Lower right corner p3x = 1130, p3y = 500
  p3x = p0x;
  p3y = p2y;
  lowerRightCornerPoint = new Point(p3x,p3y);
  System.Console.WriteLine("p3x = {0}, p3y = {1}.", p3x, p3y);

  //Initialize the ball at the starting point: subtract ball's radius so that (x,y) is the upper corner of the ball.
  x = (double)p0x-radius;
  y = (double)p0y-radius;

  //Set locations
  goButton.Location       = new Point(50,590);
  resetButton.Location    = new Point(200,590);
  exitButton.Location     = new Point(350,590);
  directionLabel.Location = new Point(970,550);

  radioContainer.Location = new Point(500,590);
  greenButton.Location    = new Point(10,20);
  yellowButton.Location   = new Point(70,20);
  blackButton.Location    = new Point(130,20);
  redButton.Location      = new Point(190,20);

  xCenterBall.Location = new Point(910,580);
  yCenterBall.Location = new Point(1000,580);

  //Set colors
  this.BackColor        = Color.Gray;
  goButton.BackColor    = Color.Yellow;
  exitButton.BackColor  = Color.Yellow;
  resetButton.BackColor = Color.Yellow;
  directionLabel.BackColor = Color.White;
  xCenterBall.BackColor = Color.White;
  yCenterBall.BackColor = Color.White;

  //Add controls to the form
  Controls.Add(goButton);
  Controls.Add(exitButton);
  Controls.Add(resetButton);
  Controls.Add(directionLabel);

  radioContainer.Controls.Add(redButton);
  radioContainer.Controls.Add(greenButton);
  radioContainer.Controls.Add(blackButton);
  radioContainer.Controls.Add(yellowButton);

  Controls.Add(radioContainer);
  Controls.Add(xCenterBall);
  Controls.Add(yCenterBall);

  welcome_location = new Point(Width/2-250,8);

  //Register the event handler.  In this case each button has an event handler, but no other
  //controls have event handlers.
  resetButton.Enabled = true;
  goButton.Enabled    = true;
  exitButton.Enabled  = true;

  //Prepare the refresh clock.  A button will start this clock ticking.
  refreshClock.Enabled = false;  //Initially this clock is stopped.
  refreshClock.Elapsed += new ElapsedEventHandler(Refresh_user_interface);

  //Prepare the ball clock.  A button will start this clock ticking.
  ballUpdateClock.Enabled = false;
  ballUpdateClock.Elapsed += new ElapsedEventHandler(Update_ball_coordinates);

  //Use extra memory to make a smooth animation.
  DoubleBuffered = true;

  goButton.Click      += new EventHandler(Go_stop);
  resetButton.Click   += new EventHandler(resetUI);
  exitButton.Click    += new EventHandler(stoprun);  //The '+' is required.
  greenButton.Click   += new EventHandler(GreenC);
  yellowButton.Click  += new EventHandler(YellowC);
  blackButton.Click   += new EventHandler(BlackC);
  redButton.Click     += new EventHandler(RedC);
}//End of constructor TrafficSignal

//Method to execute when the exit button receives an event, namely: receives a mouse click
 protected void stoprun(Object sender, EventArgs events)
 {
   Close();
 }//End of stoprun

 protected void GreenC(Object sender, EventArgs events)
 {
   ballC = BallColor.Green;
 }

 protected void YellowC(Object sender, EventArgs events)
 {
   ballC = BallColor.Yellow;
 }

 protected void BlackC(Object sender, EventArgs events)
 {
   ballC = BallColor.Black;
 }

 protected void RedC(Object sender, EventArgs events)
 {
   ballC = BallColor.Red;
 }

 protected override void OnPaint(PaintEventArgs ee)
 {
   Graphics lights = ee.Graphics;
   Pen blackPen = new Pen(Color.Black, 3);

   lights.FillRectangle(Brushes.LightGreen,0,0,Width,topPanelHeight);
   lights.DrawString(welcome_message,welcome_style,welcome_paint_brush,welcome_location);
   lights.DrawRectangle(blackPen,80,100,1050,400);

   lights.FillRectangle(Brushes.Brown,0,530,Width,topPanelHeight+130);
   lights.FillEllipse(Brushes.Red,
                      (int)System.Math.Round(x),
                      (int)System.Math.Round(y),
                      (int)System.Math.Round(2.0*radius),
                      (int)System.Math.Round(2.0*radius));

    switch(ballC)
    {
      case BallColor.Green:
      {
        lights.FillEllipse(Brushes.Green,
                           (int)System.Math.Round(x),
                           (int)System.Math.Round(y),
                           (int)System.Math.Round(2.0*radius),
                           (int)System.Math.Round(2.0*radius));
      }
        break;

      case BallColor.Yellow:
      {
        lights.FillEllipse(Brushes.Yellow,
                           (int)System.Math.Round(x),
                           (int)System.Math.Round(y),
                           (int)System.Math.Round(2.0*radius),
                           (int)System.Math.Round(2.0*radius));
      }
          break;

        case BallColor.Black:
        {
          lights.FillEllipse(Brushes.Black,
                             (int)System.Math.Round(x),
                             (int)System.Math.Round(y),
                             (int)System.Math.Round(2.0*radius),
                             (int)System.Math.Round(2.0*radius));
        }
          break;

          case BallColor.Red:
          {
            lights.FillEllipse(Brushes.Red,
                               (int)System.Math.Round(x),
                               (int)System.Math.Round(y),
                               (int)System.Math.Round(2.0*radius),
                               (int)System.Math.Round(2.0*radius));
          }
              break;
    }

    switch(orientation)
    {
      case OrientationDir.left:
        directionLabel.Text = "Left";
        break;

      case OrientationDir.right:
        directionLabel.Text = "Right";
        break;

      case OrientationDir.up:
        directionLabel.Text = "Up";
        break;

      case OrientationDir.down:
        directionLabel.Text = "Down";
        break;
    }

   //Initial state at the start and end of program
   if(refreshClock.Enabled == false && ballUpdateClock.Enabled == false)
   {
     lights.FillEllipse(Brushes.HotPink,
                        (int)System.Math.Round(x),
                        (int)System.Math.Round(y),
                        (int)System.Math.Round(2.0*radius),
                        (int)System.Math.Round(2.0*radius));
   }
   base.OnPaint(ee);
 }//END protected override void OnPaint(PaintEventArgs ee)

protected void Refresh_user_interface(System.Object sender, ElapsedEventArgs even)
{
  Invalidate();
}

protected void Update_ball_coordinates(System.Object sender, ElapsedEventArgs even)
{//This function is called each time the ball_update_clock makes one tic.  That clock is often called the animation clock.
  if(System.Math.Abs(y+radius-p0y)<0.5)    //Test if the ball is on the top horizontal line segment.
  {
    if(System.Math.Abs(x+radius-(double)p1x)>delta)  //Test if there is room to move forward
    {//If condition is true then move the ball by amount delta to the left.
     x -= delta;
     xCenterBall.Text = (x+radius).ToString();
     yCenterBall.Text = (y+radius).ToString();
    }
    else
    {//If condition is false make the ball move around the corner and start traveling down.
     y = (double)p1y+(delta-(x+radius-(double)p1x));
     x = (double)p1x-radius;
     orientation = OrientationDir.down;
    }
  }//End of if
  else if(System.Math.Abs(x+radius-(double)p1x)<0.5)  //If this is true then the ball is on the line segment from upper_left_corner_point to lower_left_corner_point
  {
    if(System.Math.Abs((double)p2y-(y+radius))>delta)
    {//If condition is true then move the ball by amount delta downward.
     y = y+delta;
     xCenterBall.Text = (x+radius).ToString();
     yCenterBall.Text = (y+radius).ToString();
    }
    else
    {//If condition is false then move the ball around the corner and begin traveling right.
     x = (double)p2x+(delta-((double)p2y-(y+radius)));
     y = (double)p2y-radius;
     orientation = OrientationDir.right;
    }//End of most recent else
  }
  else if(System.Math.Abs(y+radius-(double)p2y)<0.5)  //If this is true then the ball is on the lower line segment traveling to the right.
  {
    if(System.Math.Abs((double)p3x-(x+radius))>delta)
    {//If the condition is true then move the ball right by the amount delta
     x = x + delta;
     xCenterBall.Text = (x+radius).ToString();
     yCenterBall.Text = (y+radius).ToString();
    }
    else
    {//If the condition is false then distance between the ball and the destination point (p3x,p3y) is less than delta.  Make one last move and stop.
      y = (double)p3y-(delta-((double)p3x-(x+radius)));
      x = (double)p3x-radius;
      orientation = OrientationDir.up;
    }//End of else part
  }//End of nested ifs
  else if(System.Math.Abs(x+radius-(double)p3x)<0.5)  //If this is true then the ball is on the lower line segment traveling to the right.
  {
    if(System.Math.Abs((double)p0y-(y+radius))>delta)
      {//If condition is true then move the ball by amount delta downward.
        y = y - delta;
        xCenterBall.Text = (x+radius).ToString();
        yCenterBall.Text = (y+radius).ToString();
      }
    else
      {//If condition is false then move the ball around the corner and begin traveling right.
        x = (double)p0x-radius;
        y = (double)p0y-radius;
        refreshClock.Enabled = false;
        ballUpdateClock.Enabled = false;
        clocks_are_stopped = true;
        goButton.Text = "Go";
      }//End of most recent else
  }
}//End of method Update_ball_coordinates

protected void Go_stop(System.Object sender, EventArgs even)
{
 if(clocks_are_stopped)
 {//Start the refresh clock running.
  refreshClock.Enabled = true;
  //Start the animation clock running.
  ballUpdateClock.Enabled = true;
  //Change the message on the button
  goButton.Text = "Pause";
 }
else
 {//Stop the refresh clock.
  refreshClock.Enabled = false;
  //Stop the animation clock running.
  ballUpdateClock.Enabled = false;
  //Change the message on the button
  goButton.Text = "Go";
 }
//Toggle the variable clocks_are_stopped to be its negative
clocks_are_stopped = !clocks_are_stopped;
}//End of event handler Go_stop

 protected void resetUI(System.Object sender, EventArgs events)
 {
   refreshClock.Enabled = false;
   ballUpdateClock.Enabled = false;
   x = (double)p0x-radius;
   y = (double)p0y-radius;
 }


}//End of clas TravelingBall
