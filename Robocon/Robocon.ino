#define REMOTEXY_MODE__ESP32CORE_BLE
#include <BLEDevice.h>
#include <RemoteXY.h>

// --- Servo Configuration ---
const int SERVO_PIN = 13;
const int SERVO_CH = 7;
const int ANGLE_OFF = 0;
const int ANGLE_ON = 90;

const int SERVO2_PIN = 12;
const int SERVO2_CH = 6;
const int ANGLE2_OFF = 50;
const int ANGLE2_ON = 100;

// --- Motor Driver Configuration ---
const int IN1 = A18;
const int IN2 = A19;
const int IN3 = A17;
const int IN4 = A16;

const int CHANNEL_0 = 0;
const int CHANNEL_1 = 1;
const int CHANNEL_2 = 2;
const int CHANNEL_3 = 3;

const int LEDC_TIMER_BIT = 8;
const int LEDC_BASE_FREQ = 12800;
const int VALUE_MAX = 255;

// --- RemoteXY Configuration ---
#define REMOTEXY_BLUETOOTH_NAME "20kgMotor"

#pragma pack(push, 1)
uint8_t RemoteXY_CONF[] =
  { 255,4,0,0,0,56,0,19,0,0,0,0,31,1,106,200,1,1,3,0,
  5,22,112,60,60,0,2,26,31,2,29,30,44,22,0,2,26,31,31,79,
  78,0,79,70,70,0,2,29,68,44,22,0,2,26,31,31,79,78,0,79,
  70,70,0 };

struct {
  int8_t joystick_01_x; 
  int8_t joystick_01_y; 
  uint8_t switch_01; 
  uint8_t switch_02; 
  uint8_t connect_flag; 
} RemoteXY;
#pragma pack(pop)


// --- Functions ---

void servoWriteDeg(int deg, int channel) {
  deg = constrain(deg, 0, 180);
  int us = map(deg, 0, 180, 500, 2400);
  uint32_t duty = (uint32_t)((us / 1000000.0) * 65535.0 * 50.0);
  ledcWriteChannel(channel, duty);
}

void servoInit() {
  ledcAttachChannel(SERVO_PIN, 50, 16, SERVO_CH);
  servoWriteDeg(ANGLE_OFF, SERVO_CH);
}

void servo2Init() {
  ledcAttachChannel(SERVO2_PIN, 50, 16, SERVO2_CH);
  servoWriteDeg(ANGLE2_OFF, SERVO2_CH);
}

void forward(int pwm) {
  if (pwm > VALUE_MAX) pwm = VALUE_MAX;
  ledcWriteChannel(CHANNEL_0, pwm);
  ledcWriteChannel(CHANNEL_1, 0);
  ledcWriteChannel(CHANNEL_2, pwm);
  ledcWriteChannel(CHANNEL_3, 0);
}

void reverse(int pwm) {
  if (pwm > VALUE_MAX) pwm = VALUE_MAX;
  ledcWriteChannel(CHANNEL_0, 0);
  ledcWriteChannel(CHANNEL_1, pwm);
  ledcWriteChannel(CHANNEL_2, 0);
  ledcWriteChannel(CHANNEL_3, pwm);
}

void brake() {
  ledcWriteChannel(CHANNEL_0, VALUE_MAX);
  ledcWriteChannel(CHANNEL_1, VALUE_MAX);
  ledcWriteChannel(CHANNEL_2, VALUE_MAX);
  ledcWriteChannel(CHANNEL_3, VALUE_MAX);
}

void setup() {
  RemoteXY_Init();

  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);

  ledcAttachChannel(IN1, LEDC_BASE_FREQ, LEDC_TIMER_BIT, CHANNEL_0);
  ledcAttachChannel(IN2, LEDC_BASE_FREQ, LEDC_TIMER_BIT, CHANNEL_1);
  ledcAttachChannel(IN3, LEDC_BASE_FREQ, LEDC_TIMER_BIT, CHANNEL_2);
  ledcAttachChannel(IN4, LEDC_BASE_FREQ, LEDC_TIMER_BIT, CHANNEL_3);

  servoInit();
  servo2Init();
}

void loop() {
  RemoteXY_Handler();

  // Motor Control
  if (RemoteXY.joystick_01_y >= 30) {
    forward(RemoteXY.joystick_01_y * 2);
  } else if (RemoteXY.joystick_01_y <= -30) {
    reverse(-RemoteXY.joystick_01_y * 2);
  } else {
    brake();
  }

  // Servo Control
  int target1 = (RemoteXY.switch_01 == 1) ? ANGLE_ON : ANGLE_OFF;
  int target2 = (RemoteXY.switch_02 == 1) ? ANGLE2_ON : ANGLE2_OFF;

  servoWriteDeg(target1, SERVO_CH);
  servoWriteDeg(target2, SERVO2_CH);
}