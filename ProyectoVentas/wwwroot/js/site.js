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
let cartObjs = []

//Agrega una Prenda al array cartObjs
function agregarCarrito(id, nombre, precio, talle, descripcion, publico) { 

    if (!buscarPrenda(id)){
        cartObjs.push(new Prenda(id, nombre, precio, talle, descripcion, publico));
    } else {
        alert("El producto que intentas agregar ya esta en el carrito.")
    }
    counterCart.innerHTML = cartObjs.length;// Actualiza el contador después de agregar
    carrito.innerHTML += generarHtmlCarrito(nombre, precio, talle, descripcion, publico);
};

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

