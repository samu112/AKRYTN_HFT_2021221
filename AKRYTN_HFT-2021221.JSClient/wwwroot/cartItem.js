let cartItems = [];
let bookIds = [];
let cartIds = [];

let connection = null;

getdata();
getbookData();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8921/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CartItemCreated", (user, message) => {
        getdata();
        getbookData();
    });

    connection.on("CartItemDeleted", (user, message) => {
        getdata();
        getbookData();
    });            
                   
    connection.on("CartItemUpdated", (user, message) => {
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
    await fetch('http://localhost:8921/cartItem/')
        .then(x => x.json())
        .then(y => {
            cartItems = Object.values(y)[1];
            display();
        });
}
async function getbookData() {
    await fetch('http://localhost:8921/book/')
        .then(x => x.json())
        .then(y => {
            let books = Object.values(y)[1];
            books.forEach(t => {
                bookIds.push(t.b_id);
            })
            getcartData();
        });
}
async function getcartData() {
    await fetch('http://localhost:8921/cart/')
        .then(x => x.json())
        .then(y => {
            let carts = Object.values(y)[1];
            carts.forEach(t => {
                cartIds.push(t.c_id);
            })
            dropdownmenupopulation();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';

    cartItems.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.ci_id + "</td><td>" + t.ci_quantity + "</td><td>" + t.ci_book_id + "</td><td>" + t.ci_cart_id + "</td><td>" + `<button type="button" onclick="remove(${t.ci_id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.ci_id},${t.ci_quantity},${t.ci_book_id},${t.ci_cart_id})">Edit</button>` + "</td></tr>";

        console.log(t.ci_id);
    });
}

function showupdate(id, quantity, bookId, cartId) {
    document.getElementById('cartitemidtoupdate').value = id;
    document.getElementById('cartitemquantitytoupdate').value = quantity;
    document.getElementById('cartitembookidtoupdate').value = bookId;
    document.getElementById('cartitemcartidtoupdate').value = cartId;
    document.getElementById('updateformdiv').style.display = 'flex';
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let id = document.getElementById('cartitemidtoupdate').value;
    let quantity = document.getElementById('cartitemquantitytoupdate').value;
    let bookId = document.getElementById('cartitembookidtoupdate').value;
    let cartId = document.getElementById('cartitemcartidtoupdate').value;
    fetch('http://localhost:8921/cartItem', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                ci_id: id,
                ci_quantity: quantity,
                ci_book_id: bookId,
                ci_cart_id: cartId
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
    let quantity = document.getElementById('quantity').value;
    let bookId = document.getElementById('bookId').value;
    let cartId = document.getElementById('cartId').value;

    fetch('http://localhost:8921/cartItem', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                ci_quantity: quantity,
                ci_book_id: bookId,
                ci_cart_id: cartId
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
    fetch('http://localhost:8921/cartItem/' + id, {
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
    let select = document.getElementById("bookId");
    select.innerHTML = "";
    for (let i = 0; i < bookIds.length; i++) {
        let opt = bookIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
    select = document.getElementById("cartitembookidtoupdate");
    select.innerHTML = "";
    for (let i = 0; i < bookIds.length; i++) {
        let opt = bookIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
    select = document.getElementById("cartId");
    select.innerHTML = "";
    for (let i = 0; i < cartIds.length; i++) {
        let opt = cartIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
    select = document.getElementById("cartitemcartidtoupdate");
    select.innerHTML = "";
    for (let i = 0; i < cartIds.length; i++) {
        let opt = cartIds[i];
        let el = document.createElement("option");
        el.textContent = opt;
        el.value = opt;
        select.appendChild(el);
    }
}


