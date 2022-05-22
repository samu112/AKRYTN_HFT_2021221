let users = [];
let connection = null;

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
    await fetch('http://localhost:8921/user/')
        .then(x => x.json())
        .then(y => {
            users = Object.values(y)[1];
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    console.log(users);

    users.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.u_id + "</td><td>" + t.u_name + "</td><td>" + t.u_regDate + "</td><td>" + t.u_address + "</td><td>" + t.u_email + "</td><td>" + `<button type="button" onclick="remove(${t.u_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.u_id},'${t.u_name}','${t.u_regDate}','${t.u_address}','${t.u_email}')">Edit</button>` + "</td></tr>";

        console.log(t.u_name);
    });
}

function showupdate(id, name, regDate, address, email) {
    document.getElementById('useridtoupdate').value = id;
    document.getElementById('usernametoupdate').value = name;
    dasjdaskj = new Date(regDate)
    document.getElementById('userregdatetoupdate').value = new Date(regDate).toISOString().slice(0, 16);
    document.getElementById('useraddresstoupdate').value = address;
    document.getElementById('useremailtoupdate').value = email;
    document.getElementById('updateformdiv').style.display = 'flex';
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let id = document.getElementById('useridtoupdate').value;
    let name = document.getElementById('usernametoupdate').value;
    let regDate = document.getElementById('userregdatetoupdate').value;
    let address = document.getElementById('useraddresstoupdate').value;
    let email = document.getElementById('useremailtoupdate').value;
    fetch('http://localhost:8921/user', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                u_id: id,
                u_name: name,
                u_regDate: regDate,
                u_address: address,
                u_email: email
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error) });
}




function create() {
    let username = document.getElementById('username').value;
    let regDate = document.getElementById('regDate').value;
    let address = document.getElementById('address').value;
    let email = document.getElementById('email').value;


    fetch('http://localhost:8921/user', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {

                u_name: username,
                u_regDate: regDate,
                u_address: address,
                u_email: email
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

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
        .catch((error) => { console.error('Error:', error); });

}


