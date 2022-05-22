let books = [];
let publisherIds = [];

let connection = null;

getdata();
getbookData();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8921/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BookCreated", (user, message) => {
        getdata();
        getbookData();
    });

    connection.on("BookDeleted", (user, message) => {
        getdata();
        getbookData();
    });            
                   
    connection.on("BookUpdated", (user, message) => {
        getdata();
        getbookData();

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
    await fetch('http://localhost:8921/book/')
        .then(x => x.json())
        .then(y => {
            users = Object.values(y)[1];
            //console.log(books);
            display();
        });
}
async function getbookData() {
    await fetch('http://localhost:8921/publisher/')
        .then(x => x.json())
        .then(y => {
            let publishers = Object.values(y)[1];
            publishers.forEach(t => {
                publisherIds.push(t.p_id);
            })
            dropdownmenupopulation();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    console.log(users);

    users.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.b_id + "</td><td>" + t.b_title + "</td><td>" + t.b_author + "</td><td>" + t.b_price + "</td><td>" + t.b_releaseDate + "</td><td>" + t.b_publisher_id + "</td><td>" + `<button type="button" onclick="remove(${t.b_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.b_id},'${t.b_title}','${t.b_author}',${t.b_price},'${t.b_releaseDate}',${t.b_publisher_id})">Edit</button>` + "</td></tr>";

        console.log(t.b_title);
    });
}

function showupdate(id, title, author, price, releaseDate, publisherId) {
    document.getElementById('bookidtoupdate').value = id;
    document.getElementById('booktitletoupdate').value = title;
    document.getElementById('bookauthortoupdate').value = author;
    document.getElementById('bookpricetoupdate').value = price;
    document.getElementById('bookreleasedatetoupdate').value = new Date(releaseDate).toISOString().slice(0, 10);
    document.getElementById('bookpublisheridtoupdate').value = publisherId;
    document.getElementById('updateformdiv').style.display = 'flex';
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let id = document.getElementById('bookidtoupdate').value;
    let title = document.getElementById('booktitletoupdate').value;
    let author = document.getElementById('bookauthortoupdate').value;
    let price = document.getElementById('bookpricetoupdate').value;
    let releaseDate = document.getElementById('bookreleasedatetoupdate').value;
    let publisherId = document.getElementById('bookpublisheridtoupdate').value;
    fetch('http://localhost:8921/book', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                b_id: id,
                b_title: title,
                b_author: author,
                b_price: price,
                b_releaseDate: releaseDate,
                b_publisher_id: publisherId
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getbookData();
        })
        .catch((error) => { console.error('Error:', error) });
}




function create() {
    let bookTitle = document.getElementById('bookTitle').value;
    let author = document.getElementById('author').value;
    let price = document.getElementById('price').value;
    let releaseDate = document.getElementById('releaseDate').value;
    let publisherId = document.getElementById('publisherId').value;


    fetch('http://localhost:8921/book', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                b_title: bookTitle,
                b_author: author,
                b_price: price,
                b_releaseDate: releaseDate,
                b_publisher_id: publisherId
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getbookData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function remove(id) {
    fetch('http://localhost:8921/book/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
            getbookData();
        })
        .catch((error) => { console.error('Error:', error); });

}

function dropdownmenupopulation() {
    let select = document.getElementById("publisherId");
    select.innerHTML = "";
    for (let i = 0; i < publisherIds.length; i++) {
        let opt = publisherIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
    select = document.getElementById("bookpublisheridtoupdate");
    select.innerHTML = "";
    for (let i = 0; i < publisherIds.length; i++) {
        let opt = publisherIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
}


