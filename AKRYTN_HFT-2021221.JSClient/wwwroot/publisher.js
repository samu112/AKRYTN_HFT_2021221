let publishers = [];
let connection = null;

let publisherIdToUpdate = -1;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8921/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PublisherCreated", (user, message) => {
        getdata();
    });

    connection.on("PublisherDeleted", (user, message) => {
        getdata();
    });

    connection.on("PublisherrUpdated", (user, message) => {
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
    await fetch('http://localhost:8921/publisher/')
        .then(x => x.json())
        .then(y => {
            publishers = Object.values(y)[1];
            //console.log(publishers);
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    console.log(publishers);

    publishers.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.p_id + "</td><td>" + t.p_name + "</td><td>" + t.p_address + "</td><td>" + t.p_website + "</td><td>" + t.p_email + "</td><td>" + `<button type="button" onclick="remove(${t.p_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.p_id},'${t.p_name}','${t.p_address}','${t.p_website}','${t.p_email}')">Edit</button>` + "</td></tr>";

        console.log(t.p_name);
    });
}

function showupdate(id, name, address, website, email) {
    document.getElementById('publisheridtoupdate').value = id;
    document.getElementById('publishernametoupdate').value = name;
    document.getElementById('publisheraddresstoupdate').value = address;
    document.getElementById('publisherwebsitetoupdate').value = website;
    document.getElementById('publisheremailtoupdate').value = email;
    document.getElementById('updateformdiv').style.display = 'flex';
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let id = document.getElementById('publisheridtoupdate').value;
    let name = document.getElementById('publishernametoupdate').value;
    let address = document.getElementById('publisheraddresstoupdate').value;
    let website = document.getElementById('publisherwebsitetoupdate').value;
    let email = document.getElementById('publisheremailtoupdate').value;
    fetch('http://localhost:8921/publisher', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                p_id: id,
                p_name: name,
                p_address: address,
                p_website: website,
                p_email: email
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
    let publishername = document.getElementById('publishername').value;
    let address = document.getElementById('address').value;
    let website = document.getElementById('website').value;
    let email = document.getElementById('email').value;


    fetch('http://localhost:8921/publisher', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {

                p_name: publishername,
                p_address: address,

                p_website: website,
                p_email: email
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
    fetch('http://localhost:8921/publisher/' + id, {
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


