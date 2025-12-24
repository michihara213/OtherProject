#include <Servo.h>

int pin_trigger1 = 12;
int pin_trigger2 = 13;
int pin_echo1 = 11;
int pin_echo2 = 10;
float distance1;
float distance2;

Servo myservo1;
Servo myservo2;

void setup() {
  Serial.begin(9600);
  pinMode(pin_trigger1, OUTPUT);
  pinMode(pin_echo1, INPUT);
  pinMode(pin_trigger2, OUTPUT);
  pinMode(pin_echo2, INPUT);
  myservo1.attach(9);
  myservo1.write(0);
  myservo2.attach(8);
  myservo2.write(0);
}

void loop() {
  digitalWrite(pin_trigger1, LOW);
  delayMicroseconds(5);
  digitalWrite(pin_trigger1, HIGH);
  delayMicroseconds(10);
  digitalWrite(pin_trigger1, LOW);
  distance1 = pulseIn(pin_echo1, HIGH);

  distance1 = distance1 / 58;
  int data1 = distance1;

  digitalWrite(pin_trigger2, LOW);
  delayMicroseconds(5);
  digitalWrite(pin_trigger2, HIGH);
  delayMicroseconds(10);
  digitalWrite(pin_trigger2, LOW);
  distance2 = pulseIn(pin_echo2, HIGH);

  distance2 = distance2 / 58;
  int data2 = distance2;

  if (distance1 <= 20 || distance2 <= 20)
  {
    myservo1.write(100);
    myservo2.write(80);
  }
  else
  {
    delay(2000);
    if (distance1 > 20 && distance2 > 20) {
      myservo1.write(0);
      myservo2.write(180);
    }
  }
}