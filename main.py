import requests
from kivy.app import App
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.button import Button
from kivy.core.window import Window


# Gets the ip address of the request (user)
def get_ip():
    response = requests.get('https://api64.ipify.org?format=json').json()
    return response["ip"]

# Fetches the location of the user based on the ip address
def get_location():
    ip_address = get_ip()
    print("IP Address")
    print(ip_address)
    response = requests.get(f'https://ipapi.co/{ip_address}/json/').json()
    location_data = {
        "ip": ip_address,
        "city": response.get("city"),
        "region": response.get("region"),
        "country": response.get("country_name")
    }
    return location_data


# UI components (using Kivy)
Window.size = (720, 1280) # Most common mobile resolution according to https://www.perfecto.io/blog/how-mobile-screen-size-resolution-and-ppi-screen-affect-test-coverage

class MainWindow(BoxLayout):
    def __init__(self):
        super().__init__()
        button1 = Button(text="Hello, World?")
        self.button = button1
        self.button.bind(on_press=self.handle_button_clicked)

        self.add_widget(button1)

    def handle_button_clicked(self, event):
        self.button.text = "Hello, World"
        location = get_location()
        print("Location")
        print(location)


class MyApp(App):
    def build(self):
        self.title = "Hello, World!"
        return MainWindow()


app = MyApp()
app.run()