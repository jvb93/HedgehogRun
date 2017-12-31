# Hedgehog.run

An IoT Experiment by JVB93

**THIS README, LIKE VERSION 2 OF THIS PROJECT, IS A WORK IN PROGRESS**

## What is it?
Hedgehog.run is a site which tracks the nightly activities of our pet hedgehog using a combination of custom hardware and software. 

The goal is to explore new technologies in the IoT/Web space while also providing a means to check up on the health of our pet.

Version 1 was built in the spring of 2017 and uses an ASP.NET MVC5 site hosted in Azure to display data, and a Raspberry Pi with some custom hardware to record data. It relies heavily on proprietary code.

Due to changing technologies and some technical challenges, it is now time to re-engineer from the ground up. Hopefully we'll be able to open-source the entirety of this version.

## Wait, a Hedgehog?
Yep. Her name is Toad. She was born in early 2016. Hedgehogs are very active at night, and love running on a wheel to stay fit. We wanted to know how far Toad ran every night, so we built an IoT odometer to track her runs while monitoring ambient temperature.

 _Hedgies are very sensitive to temperature swings, so we must keep an eye on the temperature in Toad's cage._

 To our surprise, Toad can run upwards of 7 miles a night! Not bad for an animal with 2" legs. 

## Technical details
### Version 1
#### Hardware
- Raspberry Pi Zero W
- Hall Effect Sensor + Magnet
- SHT31-D Temperature/Humidity Sensor
- 3D Printed adapter to hold sensors
#### Software
- Raspbian
- Python script to collect data from sensors
- ASP.NET site for displaying data
- Microsoft Azure Hosting
- Microsoft SQL

Version 1 starts with a Raspberry Pi Zero W running Raspbian. A Hall effect sensor is attached to the Pi via GPIO which detects the presence of a magnet. Attached to the back of her wheel is a magnet, which passes by the hall effect sensor with each revolution of the wheel. Attached to the stationary portion of the wheel is the SHT31-D sensor as well. 

The Pi's main job is to simply count the number of revolutions per minute and send that data to the cloud alongside the temperature/humidity data. It's up to the ASP.NET site to do the majority of the math. 

### Version 2
#### Hardware
- ESP8266 Development Kit
- Hall Effect Sensor + Magnet
- SHT31-D Temperature/Humidity Sensor
- 3D Printed adapter to hold sensors

#### Software
- Arduino script to collect data from sensors
- ASP.NET site for displaying data
- Microsoft Azure Hosting
- Microsoft SQL
- The rest is TBD

For Version 2, we have opted to simplify. There is no need for a full 32-bit linux computer to perform such a simple data logging task. The ESP8266 SoC is better suited for this purpose. 

This area is still under active development 

## Want to build one yourself?
### Full guide coming soon
#### You will need:
- ESP8266 Development Kit such as NodeMCU or Adafruit HUZZAH
- SHT31-D Sensor
- Hall Effect Sensor + Magnets
- Somewhere to host an ASP.NET Site
- Somewhere to host the SQL Server
- Access to a 3D Printer, or 3D Printing Service
- Assorted lengths of wire
- Breadboard
- Hedgehog wheel (Preferably a Carolina Storm Bucket Wheel)
- Hedgehog


