import requests
import sys
import Adafruit_DHT
import time

url = 'http://{}/roomstats'

while True:
    try:
        humidity, temperature = Adafruit_DHT.read_retry(11, 17)
        print('Temp={0:0.1f}*  Humidity={1:0.1f}%'.format(temperature, humidity))
        myobj = {
                  "roomName": "pythonTest",
                  "temperature": temperature,
                  "humidity": humidity,
                  "temperatureUnit": 1
                }
        x = requests.post(url, json = myobj)
        print(x.text)
    except RuntimeError as e:
        # Reading doesn't always work! Just print error and we'll try again
        print("Reading from DHT failure: ", e.args)

    time.sleep(10)