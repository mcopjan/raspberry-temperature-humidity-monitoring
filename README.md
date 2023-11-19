# Temperature & Humidity monitoring on Raspbian using DHT sensors

### This solution consists of 4 components
  - The following 3 components are running in docker containers
    - [Agent](https://hub.docker.com/repository/docker/mcopjan/raspberry-hum-temp-agent/general) - python script scraping data from temperature and humidity sensor
    - [API](https://hub.docker.com/repository/docker/mcopjan/raspberry-hum-temp-api/general) - .NetCore Api
    - [MongoDB](https://hub.docker.com/r/nonoroazoro/rpi-mongo)
  - Desktop Client - WPF application presenting temperature & humidity data [netcore 6.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) needed
  - Mobile Client - MAUI app, still in developmnent
   ![Component's diagram](/Doc_Images/raspberry-temp-hum-diagram.png?raw=true "Diagram")
### Sensors compatible with this solution
 - DHT11 (tested but not recommended as humidity is not very precise, off by 20-25%)
 - DHT22 <img src="https://www.iconsdb.com/icons/preview/green/checkmark-xxl.png" width="20">

### What do you need?
 - Raspberry Pi (I am using Pi3 & Zero, one hosting API ,Agent & DB and the other only Agent)
 - DHT 22 sensor/s - you can buy them from eBay for ~ 3£ each

### Setup
 - The easiest way is to
     - Connect DHT sensor to Raspberry PI GPIO ![GPIO](/Doc_Images/Raspberry-Pi-GPIO-Header-with-Photo.png?raw=true "GPIO")
        - In my case  power to pins 2&6 and signal one into pin 11 (GPIO17)![GPIO2](/Doc_Images/20231119_173254.jpg?raw=true "GPIO2") 
     - Install docker
     - Run docker 'compose -f [docker-compose.yml](https://github.com/mcopjan/raspberry-temperature-humidity-monitoring/blob/master/docker-compose.yml) up' This will spin up API, Agent and DB
    - In case of monitoring multiple rooms (having another Raspberry Pi's with DHT sensors connected) you need run another agent and connect it set it up to send data to API. e.g.
    ```docker run --privileged -e API_URL={API_IP}:80 -e ROOM_NAME=demo_room -e SENSOR_MODEL=22 -e GPIO_PIN=17 -e INTERVAL_SEC=60 mcopjan/raspberry-hum-temp-agent:latest```
 - 

