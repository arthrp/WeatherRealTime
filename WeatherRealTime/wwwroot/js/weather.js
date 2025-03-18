console.log('here!');

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/weatherHub")
    .build();

const divElem = document.getElementById("weather-result");

connection.start()
    .then(() => {
        console.log("Connected to SignalR hub.");
        // Call the JoinGroup method on the hub, passing the group name
        connection.invoke("JoinGroup")
            .then(() => console.log("Joined group: WeatherUpdates"))
            .catch(err => console.error("Error joining group:", err));
        
        connection.on("UpdateWeather", (x) => {
            console.log('got!', x);
            document.getElementById("weather-result").appendChild(Object.assign(document.createElement('span'), { textContent: `Wind: ${x.windSpeed} m/s` }));
            document.getElementById("weather-result").appendChild(document.createElement('br'));
        })
    })
    .catch(err => console.error("Error establishing connection:", err));