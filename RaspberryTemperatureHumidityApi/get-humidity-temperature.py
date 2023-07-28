import requests
import sys
import Adafruit_DHT
import time

url = sys.argv[1]
sensor = sys.argv[2]
pin = sys.argv[3]
roomName = sys.argv[4]
intervalInSec = sys.argv[5]

#sudo python3 send_post.py 192.168.5.199:5144 11 17 homeOffice 10

while True:
    try:
        humidity, temperature = Adafruit_DHT.read_retry(int(11),int(17))
        print('Temp={0:0.1f}*  Humidity={1:0.1f}%'.format(temperature, humidity))
        myobj = {
                  "roomName": roomName,
                  "temperature": temperature,
                  "humidity": humidity,
                  "temperatureUnit": 1
                }
        x = requests.post('http://{0}/roomstats'.format(url), json = myobj)
        print(x.text)
    except RuntimeError as e:
        print("Reading from DHT failure: ", e.args)

    time.sleep(int(intervalInSec))




