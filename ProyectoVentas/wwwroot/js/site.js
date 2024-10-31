const counterCart = document.getElementById("counterCart")
const carrito = document.getElementById("carrito")
const cantProds = document.getElementById("prods")
const total = document.getElementById("precio")
class Prenda {
    constructor(id, nombre, precio, talle, descripcion, publico) { 
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.talle = talle;
        this.descripcion = descripcion;
        this.publico = publico;
    }

    mismoId(id) {
        return this.id == id;
    }
}
let cartObjs = [];

//Agrega una Prenda al array cartObjs.
function agregarCarrito(id, nombre, precio, talle, descripcion, publico) { 

    cartObjs = JSON.parse(localStorage.getItem("cartObjs"))//Trae el carrito de la localStorage.
    //Chequea que la prenda no exista en el carrito.
    if (!buscarPrenda(id)) {
        //Si no existe la agrega.
        cartObjs.push(new Prenda(id, nombre, precio, talle, descripcion, publico));
        localStorage.setItem("cartObjs", JSON.stringify(cartObjs)) //Guarda el array en la memoria del navegador.
        counterCart.innerHTML = JSON.parse(localStorage.getItem("cartObjs")).length;// Actualiza el contador.
        generarHtmlCarrito()
    } else {
        //Si existe lo notifica mediante alerta.
        //TODO-- Sustituir la alerta por un mensaje en pantalla.
        alert("El producto que intentas agregar ya esta en el carrito.")
    }
    actualizarCantidadProductos()
    actualizarPrecioTotal()
};

//Busca la prenda en el array del carrito que trae de la memoria.
let buscarPrenda = (id) => {
    let encontrado = false
    let i = 0
    let arrayMemoria = JSON.parse(localStorage.getItem("cartObjs"))
    while (!encontrado && i < arrayMemoria.length) {
        if (arrayMemoria[i].id == id){
            encontrado = true;
        }
        i++;
    }

    return encontrado;
};

//Genera el HTML que se inserta en el carrito.
function generarHtmlCarrito() {
    counterCart.innerHTML = JSON.parse(localStorage.getItem("cartObjs")).length;// Actualiza el contador.
    let carritoMemoria = JSON.parse(localStorage.getItem("cartObjs"));// Trae el carrito en localStorage.
    let arrDibujado = [] //Array de los elementos que actualmente se muestran en pantalla.
    carritoMemoria.forEach(function (prenda) {
        //Chequea si la prenda que esta por dibujar ya esta dibujada y dado el caso no la dibuja.
        if (!arrDibujado.includes(prenda.id)) {

            carrito.innerHTML +=
                `
        <div class="tarjeta">
                <div>
                    <p>Nombre</p>
                    <p>${prenda.nombre}</p>
                </div>
                <div>
                    <p>Precio</p>
                    <p>${prenda.precio}</p>
                </div>
                <div>
                    <p>Talle</p>
                    <p>${prenda.talle}</p>
                </div>
                <div>
                    <p>Descripcion</p>
                    <p>${prenda.descripcion}</p>
                </div>
                <div>
                    <p>Publico</p>
                    <p>${prenda.publico}</p>
                </div>
                <div>
                    <p>Cantidad</p>
                    <input type="number" id="inp-number" min="1" placeholder="0">
                </div>
                <button onclick="removePrendaCarrito(${prenda.id})">Eliminar</button>
        </div>
        `
        arrDibujado.push(prenda.id) // Una vez dibujada la prenda la agrega al array de dibujados.
        }
    }
    )}

function removePrendaCarrito(id) {

    let array = JSON.parse(localStorage.getItem("cartObjs")); // Trae el carrito de la localStorage.
    
    let index = array.findIndex(prenda => prenda.id == id) //Encuentra el indice de la prenda con el id a remover.
    array.splice(index,1) // Elimina la prenda del array pasando por parametro el indice encontrado.
    localStorage.setItem("cartObjs", JSON.stringify(array)) // Vuelve a guardar el array en memoria.

    counterCart.innerHTML = JSON.parse(localStorage.getItem("cartObjs")).length;// Actualiza el contador.
    actualizarCantidadProductos()
    actualizarPrecioTotal()
    location.reload() // Recarga la pagina para ver los cambios en el carrito.

}

function actualizarCantidadProductos() {
    let array = JSON.parse(localStorage.getItem("cartObjs"))
    let cant = 0
    array.forEach(function () {
        cant += 1
    })
    cantProds.innerHTML += cant;
}

function actualizarPrecioTotal() {
    let array = JSON.parse(localStorage.getItem("cartObjs"))
    let precioTotal = 0
    array.forEach(function (prenda) {
        precioTotal += prenda.precio
    })
    total.innerHTML += precioTotal
}

generarHtmlCarrito();//Agrega las tarjetas al carrito.
actualizarCantidadProductos()
actualizarPrecioTotal()