let users = [];
let connection = null;

let movieIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8921/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("UserCreated", (user, message) => {
        getdata();
    });

    connection.on("UserDeleted", (user, message) => {
        getdata();
    });

    connection.on("UserUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    fetch('http://localhost:8921/user')
        .then(x => x.json())
        .then(y => {
            users = y
            //console.log(maps);
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    users.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.u_id + "</td><td>" + t.u_name + "</td><td>" + t.u_regDate + "</td><td>" + t.u_address + "</td><td>" + t.u_email + "</td><td>" +`<button type="button" onclick="remove(${t.u_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.u_id})">Edit</button>` + "</td></tr>";
        console.log(t.u_name);
    });
}

function showupdate(id) {
    //alert(id);
    document.getElementById('usernametoupdate').value = users.find(t => t['u_id'] == id)['u_name'];

    document.getElementById('updateformdiv').style.display = 'flex';
    userIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('usernametoupdate').value;

    fetch('http://localhost:8921/user', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { u_id: userIdToUpdate, u_name: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let username = document.getElementById('username').value;
    let regdate = document.getElementById('regdate').value;
    let address = document.getElementById('address').value;
    let email = document.getElementById('email').value;

    var data = { "UserName": username, "RegistrationDate": regdate, "Address": address, "Email": email };
    $.ajax({
        url: 'http://localhost:8921/user',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function (data) {
            console.log(data);
        }
    });
    //fetch('http://localhost:37827/movie', {
    //    method: 'POST',
    //    headers: { 'Content-Type': 'application/json', },
    //    body: JSON.stringify(
    //        { MovieName: moviename, Language: language, Duration: duration, Type: type, Date: date }),
    //})
    //    .then(response => response)
    //    .then(data => {
    //        console.log('Success:', data);
    //        getdata();
    //    })
    //    .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:8921/user/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); alert("This instance is connected to something else in the database, first you should delete them as well") });
}