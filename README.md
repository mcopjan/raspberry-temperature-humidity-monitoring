# raspberry-temperature-humidity-monitoring

This solution was created to monitor temperature and humidity in my house. It consists of 3 components
- Agent - python script running inside a docker container scraping data from temperature and humidity sensor and sending data to API
- API - .NetCore Api running inside a docker container storing & reading data in/from DB
- MongoDB - database running inside a docker container
- Desktop Client - WPF application presenting temperature & humidity data
- Mobile Client - MAUI app, still in developmnent

Sensors compatible with this solution are 
 - DHT11 (tested but not recommended as humidity is not very precise, off by 20-25%)
 - DHT22 (tested and recommended)
 
 I needed to monitor 2 rooms in my house so my current setup is
  - Room1 - Raspberry Pi 3 - this is running 3 containers - API, DB & Agent
  - Room2 - Raspberry Zero - Agent
