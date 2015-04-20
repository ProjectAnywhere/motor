/* Sweep
 by BARRAGAN <http://barraganstudio.com> 
 This example code is in the public domain.

 modified 8 Nov 2013
 by Scott Fitzgerald
 http://arduino.cc/en/Tutorial/Sweep
*/ 

#include <Servo.h> 
 
Servo myservo1;  // create servo object to control a servo 
                // twelve servo objects can be created on most boards
Servo myservo2;

int pos1 = 0;    // variable to store the servo position 
//int pos2 = 0;

void setup() 
{ 
  myservo1.attach(9);  // attaches the servo on pin 9 to the servo object 
  myservo2.attach(5);
} 
 
void loop() 
{ 
  
  for(pos1 = 0; pos1 <= 180; pos1 += 1) // goes from 0 degrees to 180 degrees 
  {                                  // in steps of 1 degree 
    myservo2.write(pos1);
    myservo1.write(pos1);              // tell servo to go to position in variable 'pos' 
    delay(15);                       // waits 15ms for the servo to reach the position 
  } 
  for(pos1 = 180; pos1>=0; pos1-=1)     // goes from 180 degrees to 0 degrees 
  {
    myservo2.write(pos1);    
    myservo1.write(pos1);              // tell servo to go to position in variable 'pos' 
    delay(15);                       // waits 15ms for the servo to reach the position 
  } 
} 

