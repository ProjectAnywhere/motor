
#include <Servo.h> 
Servo myservo;
int pos = 0;

String comdata = "";
int cmd[2] = {0}, mark = 0;

void setup() {  
   Serial.begin (9600);  
   myservo.attach(9); 
}


void loop() {
  int j = 0;
  while (Serial.available() > 0) {
    comdata += char(Serial.read());
    delay(2);
    mark = 1;
  }

  if(mark == 1) {
    for(int i = 0; i < comdata.length() ; i++) {
      if(comdata[i] == ',') {
        j++;
      }
      else {
        cmd[j] = (comdata[i] - '0');
      }
    }
    comdata = String("");    
  }
  
  if(cmd[0]!=0) {
    for(pos = 0; pos <= cmd[0]*10; pos += 1) { // goes from 0 degrees to 180 degrees, in steps  of 1 degree 
      myservo.write(pos);
      delay(15);  // waits 15ms for the servo to reach the position 
    }
  }
}

