#include <Arduino.h>
#include <Wire.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <ESP8266WiFi.h>
#include "Adafruit_SHT31.h";
#include "secrets.h";

Adafruit_SHT31 sht31 = Adafruit_SHT31();
HTTPClient http;

const char* host = HOST;
const char* ssid     = SSID;
const char* password = WIFI_PASS;
const byte interruptPin = 0;
volatile int tickCount = 0;

void setup()
{
  Serial.begin(9600);
  
  //try to connect to temp sensor
  if (! sht31.begin(0x44))
  {
    Serial.println("Couldn't find SHT31");
    ESP.reset();//reboot, try again
  }

  //setup hall effect sensor
  pinMode(interruptPin, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(interruptPin), handleInterrupt, RISING);
  
  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  //connect to wifi
  WiFi.begin(ssid, password);

  //wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP()); 
}


void loop()
{
  //if wifi was disconnected, just reboot
  if (WiFi.status() == 6)
  {
    ESP.reset();
  }
  //get temperature and humidity
  float t = sht31.readTemperature();
  float h = sht31.readHumidity();

  //make sure temp and humidity exist
  if (! isnan(t) && ! isnan(h))
  {
    //build JSON payload
    StaticJsonBuffer<200> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root["TemperatureF"] = (t * 9.0) / 5.0 + 32;;
    root["Ticks"] = tickCount;
    root["Humidity"] = h;  
    String data = "";
    root.printTo(data);
    Serial.print(data);
    
    Serial.println("Requesting POST: ");
    //start http connection
    http.begin(host);
    http.addHeader("Content-Type", "application/json");
    //send data to server
    int httpCode = http.POST(data);
    String payload = http.getString();                
    
    Serial.println(httpCode);   //Print HTTP return code
    Serial.println(payload);    //Print request response payload
    
    http.end();  //Close connection
  }

  //reset tick count 
  tickCount = 0; 
  //wait one minute
  delay(60000);
}

//handler for when hall effect is triggered, add one to the count of ticks per this period
void handleInterrupt() {
  tickCount++;
}

