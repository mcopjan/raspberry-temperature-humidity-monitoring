from flask import Flask, jsonify
import Adafruit_DHT
import sys

app = Flask(__name__)

# Set the type of sensor and the GPIO pin number
# sudo python3 sensor-server.py 22 17
DHT_SENSOR_TYPE = sys.argv[1]
DHT_SENSOR_PIN = sys.argv[2]


@app.route('/sensor', methods=['GET'])
def get_sensor_data():
    humidity, temperature = Adafruit_DHT.read_retry(int(DHT_SENSOR_TYPE), int(DHT_SENSOR_PIN))
    if humidity is not None and temperature is not None:
        return jsonify({'temperature': temperature, 'humidity': humidity})
    else:
        return jsonify({'error': 'Failed to retrieve data from sensor'}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
