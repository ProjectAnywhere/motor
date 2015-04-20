
/*
 Created on 18th April 2015
 by Ekta Bindlish
 
 This program drives a bipolar stepper motor.

 The 1st motor is attached to digital pins 8 - 11 of the Arduino.
 The 2nd motor is attached to digital pins 4 -  7 of the Arduino.

 The motor will step one step at a time. 
 Each step covers 7.5 degrees.
 One rotation takes 48 steps.

 The program takes a 6 byte serial message as input
 Parses it to get the direction of rotation, and number of steps

 Serial message format:
 byte0: Motor A's direction, 0:clockwise, 1:anticlockwise
 byte1: Motor A's number of steps, MSB
 byte2: Motor A's number of steps, LSB
 byte3: Motor B's direction, 0:clockwise, 1:anticlockwise
 byte4: Motor B's number of steps, MSB
 byte5: Motor B's number of steps, LSB

 example: 012113
 rotate Motor A clockwise by 12 steps
 rotate Motor B anticlockwise 13 steps
 
 */

#include <Stepper.h>

int stepA;
int stepB;

char directionA;
char directionB;

char buffer1[100];

// the stepsPerRevolution and degreesPerStep were taken from the motor specs
const int stepsPerRevolution = 48;  // motor takes 48 steps per minute
const double degreesPerStep = 7.5;  // motor takes 7.5 degrees per step

// initialize stepper motor A on pins 8 through 11:
//for clockwise rotation
//brown:  pin 8
//black:  pin 9
//orange: pin 10 
//yellow: pin 11 
//no other connections needed
Stepper myStepperA(stepsPerRevolution, 8, 9, 10, 11);

// initialize stepper motor A on pins 8 through 11:
//for anti-clockwise rotation
//brown:  pin 8
//black:  pin 9
//orange: pin 10 
//yellow: pin 11 
//no other connections needed
Stepper myStepperA1(stepsPerRevolution, 10, 11, 8, 9);

// initialize stepper motor B on pins 4 through 7:
//for clockwise rotation
//brown:  pin 4
//black:  pin 5
//orange: pin 6 
//yellow: pin 7 
//no other connections needed
Stepper myStepperB(stepsPerRevolution, 4, 5, 6, 7);

// initialize stepper motor B on pins 4 through 7:
//for anti-clockwise rotation
//brown:  pin 4
//black:  pin 5
//orange: pin 6 
//yellow: pin 7 
//no other connections needed
Stepper myStepperB1(stepsPerRevolution, 6, 7, 4, 5);

// rotate motor A with n number of steps clockwise
void rotateStepperAclockwise(int numStepsA)
{
  int a;
  for (a = 0; a < numStepsA; a++)
  {
    myStepperA.step(1);
    delay(500);
  }
}

// rotate motor A with n number of steps anti-clockwise
void rotateStepperAanticlock(int numStepsA1)
{
  int a;
  for (a = 0; a < numStepsA1; a++)
  {
    myStepperA1.step(1);
    delay(500);
  }
}


//rotate motor B with n number of steps clockwise
void rotateStepperBclockwise(int numStepsB)
{
  int b;
  for (b = 0; b < numStepsB; b++)
  {
    myStepperB.step(1);
    delay(500);
  }  
}

//rotate motor B with n number of steps anti-clockwise
void rotateStepperBanticlock(int numStepsB1)
{
  int b;
  for (b = 0; b < numStepsB1; b++)
  {
    myStepperB1.step(1);
    delay(500);
  }  
}

void setup() 
{
  //initialize serial port
  Serial.begin(9600);
  Serial.setTimeout(1000);
}

void loop() 
{  
  if (Serial.available() > 0)
  {
    Serial.readBytes(buffer1,6);

    Serial.print("I received: ");
    for (int i = 0; i< 6; i++)
    {
      Serial.print(buffer1[i]);
    }
    
    Serial.println();
    
    directionA = buffer1[0] - 48;
    directionA = directionA % 2;
    directionB = buffer1[3] - 48;
    directionB = directionB % 2;
    
    stepA = ((buffer1[1] - 48)*10) + (buffer1[2] - 48);
    stepB = ((buffer1[4] - 48)*10) + (buffer1[5] - 48);
    
    Serial.print("turn motor A by: ");
    Serial.print(stepA,DEC);
    Serial.print(" steps, direction: ");
    (directionA == 0)? Serial.print("clockwise"):Serial.print("anticlockwise");
    Serial.println();

    Serial.print("turn motor B by: ");
    Serial.print(stepB,DEC);
    Serial.print(" steps, direction: ");
    (directionB == 0)? Serial.print("clockwise"):Serial.print("anticlockwise");
    Serial.println();
    
    if (stepA>0)
    {
      if (directionA)
      {
        rotateStepperAclockwise(stepA);
      }
      else
      {
        rotateStepperAanticlock(stepA);
      }
    }
    
    if (stepB > 0)    
    {
      if (directionB)
      {
        rotateStepperBclockwise(stepB);
      }
      else
      {
        rotateStepperBanticlock(stepB);
      }
    }
    
  } //end serial 
}//end loop

