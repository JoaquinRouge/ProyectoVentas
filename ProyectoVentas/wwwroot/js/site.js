let counterCart = document.getElementById("counterCart")
let carrito = document.getElementById("carrito")
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

//Agrega una Prenda al array cartObjs
function agregarCarrito(id, nombre, precio, talle, descripcion, publico) { 

    //Chequea que la prenda no exista en el carrito.
    if (!buscarPrenda(id)) {
        //Si no existe la agrega.
        cartObjs.push(new Prenda(id, nombre, precio, talle, descripcion, publico));
    } else {
        //Si existe lo notifica mediante alerta.
        //TODO-- Sustituir la alerta por un mensaje en pantalla.
        alert("El producto que intentas agregar ya esta en el carrito.")
    }
    counterCart.innerHTML = cartObjs.length;// Actualiza el contador después de agregar.
    //Agrega la tarjeta al carrito.
    carrito.innerHTML += generarHtmlCarrito(nombre, precio, talle, descripcion, publico);
};

//Busca la prenda en el array del carrito (cartObjs)
let buscarPrenda = (id) => {
    let encontrado = false
    let i = 0
    while (!encontrado && i < cartObjs.length) {
        if (cartObjs[i].mismoId(id)){
            encontrado = true
        }
        i++
    }

    return encontrado;
};

//Genera el HTML que se inserta en el carrito.
function generarHtmlCarrito(nombre, precio, talle, descripcion, publico) {
    return `
    <div class="tarjeta">
            <div>
                <p>Nombre</p>
                <p>${nombre}</p>
            </div>
            <div>
                <p>Precio</p>
                <p>${precio}</p>
            </div>
            <div>
                <p>Talle</p>
                <p>${talle}</p>
            </div>
            <div>
                <p>Descripcion</p>
                <p>${descripcion}</p>
            </div>
            <div>
                <p>Publico</p>
                <p>${publico}</p>
            </div>
    </div>
    `
}

