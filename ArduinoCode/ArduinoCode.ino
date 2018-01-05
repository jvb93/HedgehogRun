#include <Arduino.h>
#include <Wire.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <ESP8266WiFi.h>
#include "Adafruit_SHT31.h";
#include "secrets.h";

Adafruit_SHT31 sht31 = Adafruit_SHT31();
HTTPClient http;

// set up the 'counter' feed
// set up the 'temperature' and 'humidity' feeds
//AdafruitIO_Feed *temperature = io.feed("temperature");
//AdafruitIO_Feed *humidity = io.feed("humidity");


const char* host = "http://hedgehog.run/api/hoglog";
const char* ssid     = SSID;
const char* password = WIFI_PASS;


void setup()
{
  Serial.begin(9600);
  if (! sht31.begin(0x44))
  {
    Serial.println("Couldn't find SHT31");
    while (1) delay(1);
  }
  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

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
  
  float t = sht31.readTemperature();
  float h = sht31.readHumidity();

  if (! isnan(t))
  {
    Serial.print("Temp *C = "); Serial.println(t);
  }
  else
  {
    Serial.println("Failed to read temperature");
  }

  if (! isnan(h))
  {
    Serial.print("Hum. % = "); Serial.println(h);
  }
  else
  {
    Serial.println("Failed to read humidity");
  }
  
 

   StaticJsonBuffer<200> jsonBuffer;
   JsonObject& root = jsonBuffer.createObject();
   root["TemperatureF"] = (t * 9.0) / 5.0 + 32;;
    root["Ticks"] = 0;
        root["Humidity"] = h;


   String data = "";
   root.printTo(data);
   Serial.print(data);
   Serial.println("Requesting POST: ");
  http.begin(host);
  http.addHeader("Content-Type", "application/json");
  int httpCode = http.POST(data);
   String payload = http.getString();                  //Get the response payload
 
   Serial.println(httpCode);   //Print HTTP return code
   Serial.println(payload);    //Print request response payload
 
   http.end();  //Close connection
 
 
  delay(60000);
}


