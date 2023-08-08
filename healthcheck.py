import Adafruit_DHT
import sys

def read_sensor_data(sensor_type, pin_number):
    try:
        humidity, temperature = Adafruit_DHT.read_retry(sensor_type, pin_number)
        if humidity is not None and temperature is not None:
            return True, humidity, temperature
        else:
            return False, None, None
    except Exception as e:
        print("Error reading sensor data:", str(e))
        return False, None, None

if __name__ == "__main__":
    # Define the pin numbers for your sensor data and power pins
    sensor_type = sys.argv[1]
    gpio_pin = sys.argv[2]

    success, humidity, temperature = read_sensor_data(int(sensor_type), int(gpio_pin))

    if success:
        print("Humidity: {:.2f}%, Temperature: {:.2f}°C".format(humidity, temperature))
        exit(0)  # Return 0 for successful execution
    else:
        print("Failed to obtain humidity and temperature data.")
        exit(1)  # Return 1 for an error condition

