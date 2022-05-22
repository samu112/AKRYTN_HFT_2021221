let carts = [];
let userIds = [];

let connection = null;

getdata();
getuserData();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8921/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CartCreated", (user, message) => {
        getdata();
        getuserData();
    });

    connection.on("CartDeleted", (user, message) => {
        getdata();
        getuserData();
    });            
                   
    connection.on("CartUpdated", (user, message) => {
        getdata();
        getuserData();

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
    await fetch('http://localhost:8921/cart/')
        .then(x => x.json())
        .then(y => {
            carts = Object.values(y)[1];
            display();
        });
}
async function getuserData() {
    await fetch('http://localhost:8921/user/')
        .then(x => x.json())
        .then(y => {
            let users = Object.values(y)[1];
            users.forEach(t => {
                userIds.push(t.u_id);
            })
            dropdownmenupopulation();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';

    carts.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.c_id + "</td><td>" + t.c_creditcardNumber + "</td><td>" + t.c_billingAddress + "</td><td>" + t.c_deliver + "</td><td>" + t.c_user_id + "</td><td>" + `<button type="button" onclick="remove(${t.c_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.c_id},'${t.c_creditcardNumber}','${t.c_billingAddress}','${t.c_deliver}',${t.c_user_id})">Edit</button>` + "</td></tr>";

        console.log(t.c_creditcardNumber);
    });
}

function showupdate(id, creditcardNumber, billingAddress, deliver, userId) {
    document.getElementById('cartidtoupdate').value = id;
    document.getElementById('cartcreditcardnumbertoupdate').value = creditcardNumber;
    document.getElementById('cartbillingaddresstoupdate').value = billingAddress;
    let devilerycheckbox = document.getElementById('cartdeliveryrequestedtoupdate');
    if (deliver == "true") { devilerycheckbox.checked = true; }
    else { devilerycheckbox.checked = false;}
    document.getElementById('cartuseridtoupdate').value = userId;
    document.getElementById('updateformdiv').style.display = 'flex';
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let id = document.getElementById('cartidtoupdate').value;
    let creditcardNumber = document.getElementById('cartcreditcardnumbertoupdate').value;
    let billingAddress = document.getElementById('cartbillingaddresstoupdate').value;
    let deliver = document.getElementById('cartdeliveryrequestedtoupdate').checked;
    let userId = document.getElementById('cartuseridtoupdate').value;
    fetch('http://localhost:8921/cart', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                c_id: id,
                c_creditcardNumber: creditcardNumber,
                c_billingAddress: billingAddress,
                c_deliver: deliver,
                c_user_id: userId
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getuserData();
        })
        .catch((error) => { console.error('Error:', error) });
}




function create() {
    let creditcardNumber = document.getElementById('creditcardNumber').value;
    let billingAddress = document.getElementById('billingAddress').value;
    let deliver = document.getElementById('deliver').checked;
    let userId = document.getElementById('userId').value;


    fetch('http://localhost:8921/cart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                c_creditcardNumber: creditcardNumber,
                c_billingAddress: billingAddress,
                c_deliver: deliver,
                c_user_id: userId
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getuserData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function remove(id) {
    fetch('http://localhost:8921/cart/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getuserData();
        })
        .catch((error) => { console.error('Error:', error); });

}

function dropdownmenupopulation() {
    let select = document.getElementById("userId");
    select.innerHTML = "";
    for (let i = 0; i < userIds.length; i++) {
        let opt = userIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
    select = document.getElementById("cartuseridtoupdate");
    select.innerHTML = "";
    for (let i = 0; i < userIds.length; i++) {
        let opt = userIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
}


