# Temperature & Humidity Monitoring on Raspbian using DHT Sensors

## Overview

This solution comprises four main components:

- The following three components are running in Docker containers:
  - [**Agent**](https://hub.docker.com/repository/docker/mcopjan/raspberry-hum-temp-agent/general): A Python script that scrapes data from temperature and humidity sensors.
  - [**API**](https://hub.docker.com/repository/docker/mcopjan/raspberry-hum-temp-api/general): A .NET Core API.
  - [**MongoDB**](https://hub.docker.com/r/nonoroazoro/rpi-mongo): The database.

- **Desktop Client**: A WPF application presenting temperature and humidity data. Requires [.NET Core 6.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

- **Mobile Client**: A MAUI app, still in development.

![Component's Diagram](/Doc_Images/raspberry-temp-hum-diagram.png?raw=true "Diagram")

## Supported Sensors

This solution is compatible with the following sensors:

- **DHT11**: Tested but not recommended due to imprecise humidity readings (off by 20-25%).
- **DHT22** <img src="https://www.iconsdb.com/icons/preview/green/checkmark-xxl.png" width="20"> 

## Requirements

To implement this solution, you will need:

- **Raspberry Pi**: (Tested on Pi3 & Zero; one hosts API, Agent, and DB, and the other hosts only Agent).
- **DHT 22 Sensors**: Available for purchase on eBay for approximately £3 each.

## Setup

The easiest way to set up this solution is as follows:

1. **Connect DHT Sensor to Raspberry Pi GPIO:**
   - Power pins 2 & 6 and signal pin into pin 11 (GPIO17, configurable when running the Agent's Docker container).
   ![GPIO](/Doc_Images/Raspberry-Pi-GPIO-Header-with-Photo.png?raw=true "GPIO")
   ![GPIO2](/Doc_Images/20231119_173254.jpg?raw=true "GPIO2")

2. **Install Docker.**

3. **Run Docker Compose:**
   - Execute 'docker-compose -f [docker-compose.yml](https://github.com/mcopjan/raspberry-temperature-humidity-monitoring/blob/master/docker-compose.yml) up'. This will spin up the API, Agent, and DB.

4. **Monitoring Multiple Rooms:**
   - If monitoring multiple rooms (using additional Raspberry Pi's with DHT sensors), run another Agent and connect it to send data to the API. For example:
   ```bash
   docker run --privileged -e API_URL={API_IP}:80 -e ROOM_NAME=demo_room -e SENSOR_MODEL=22 -e GPIO_PIN=17 -e INTERVAL_SEC=60 mcopjan/raspberry-hum-temp-agent:latest
