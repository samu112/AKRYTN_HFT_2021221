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
            publishers = y;
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
            "<tr><td>" + t.p_id + "</td><td>" + t.p_name + "</td><td>" + t.p_address + "</td><td>" + t.p_website + "</td><td>" + t.p_email + "</td><td>" + `<button type="button" onclick="remove(${t.p_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.p_id})">Edit</button>` + "</td></tr>";

        console.log(t.p_name);
    });
}

function showupdate(id) {
    document.getElementById('publishernametoupdate').value = publishers.find(t => t['p_Id'] == id)['p_name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    publisherIdToUpdate = id;

}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('publishernametoupdate').value;
    fetch('http://localhost:8921/publisher', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { p_id: publisherIdToUpdate, p_name: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error) });
}




function create() {
    let cashiername = document.getElementById('cashiername').value;
    let address = document.getElementById('address').value;
    let bankaccount = document.getElementById('bankaccount').value;
    let insurance = document.getElementById('insurance').value;
    let salary = Number(document.getElementById('salary').value);


    fetch('http://localhost:37827/cashier', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {

                cashierName: cashiername,
                address: address,

                bankaccount: bankaccount,
                insurance: insurance,
                salary: salary
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
    fetch('http://localhost:37827/cashier/' + id, {
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


