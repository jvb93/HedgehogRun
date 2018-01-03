#include <Arduino.h>
#include <Wire.h>
#include "Adafruit_SHT31.h";
#include "Secrets.h";
#include "config.h";

Adafruit_SHT31 sht31 = Adafruit_SHT31();

int count = 0;

// set up the 'counter' feed
// set up the 'temperature' and 'humidity' feeds
AdafruitIO_Feed *temperature = io.feed("temperature");
AdafruitIO_Feed *humidity = io.feed("humidity");


void setup()
{
  Serial.begin(9600);
  if (! sht31.begin(0x44))
  {
    Serial.println("Couldn't find SHT31");
    while (1) delay(1);
  }
  Serial.print("Connecting to Adafruit IO");

  // connect to io.adafruit.com
  io.connect();

  // wait for a connection
  while (io.status() < AIO_CONNECTED) {
    Serial.print(".");
    delay(500);
  }

  // we are connected
  Serial.println();
  Serial.println(io.statusText());
}


void loop()
{
  io.run();
  float t = sht31.readTemperature();
  float h = sht31.readHumidity();

  if (! isnan(t))
  {
    Serial.print("Temp *C = "); Serial.println(t);
    temperature->save(t);

  }
  else
  {
    Serial.println("Failed to read temperature");
  }

  if (! isnan(h))
  {
    Serial.print("Hum. % = "); Serial.println(h);
    humidity->save(h);
  }
  else
  {
    Serial.println("Failed to read humidity");
  }
  Serial.println();
  delay(5000);
}


