const counterCart = document.getElementById("counterCart")
const carrito = document.getElementById("carrito")
const cantProds = document.getElementById("prods")
const total = document.getElementById("precio")

// Clase Prenda:
class Prenda {
    constructor(id, nombre, precio, talle, descripcion, publico,cantidad) { 
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.talle = talle;
        this.descripcion = descripcion;
        this.publico = publico;
        this.cantidad = cantidad;
    }

    mismoId(id) {
        return this.id == id;
    }
}
let cartObjs = [];

//Agrega una Prenda al array cartObjs.
function agregarCarrito(id, nombre, precio, talle, descripcion, publico,cantidad) { 

    cartObjs = JSON.parse(localStorage.getItem("cartObjs"))//Trae el carrito de la localStorage.
    //Chequea que la prenda no exista en el carrito.
    if (!buscarPrenda(id)) {
        //Si no existe la agrega.
        cartObjs.push(new Prenda(id, nombre, precio, talle, descripcion, publico,cantidad));
        localStorage.setItem("cartObjs", JSON.stringify(cartObjs)) //Guarda el array en la memoria del navegador.
        counterCart.innerHTML = JSON.parse(localStorage.getItem("cartObjs")).length;// Actualiza el contador.
        generarHtmlCarrito()
    } else {
        //Si existe lo notifica mediante alerta.
        //
        //
        //TODO-- Sustituir la alerta por un mensaje en pantalla.
        //
        //
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
                    <input type="number" id="inp-number-${prenda.id}" class="inp-number" min="1" 
                    placeholder="${prenda.cantidad}" value="${prenda.cantidad}">
                </div>
                <button onclick="removePrendaCarrito(${prenda.id})">Eliminar</button>
        </div>
        `
        arrDibujado.push(prenda.id) // Una vez dibujada la prenda la agrega al array de dibujados.
        }
    }
    )

    //Agrega el evento para que al incrementar o decrementar el input de cantidad pedida se actualice el precio
    //total de los productos y la cantidad total de los productos.
    carritoMemoria.forEach(function (prenda) {
        let inputCant = document.getElementById(`inp-number-${prenda.id}`) // Selecciona el input desde el body.
        inputCant.addEventListener("input", function () {
            prenda.cantidad = inputCant.value // Setea el nuevo valor de cantidad al objeto de la prenda.

            localStorage.setItem("cartObjs", JSON.stringify(carritoMemoria)) // Actualiza la prenda en memoria.

            actualizarPrecioTotal() // Actualiza el precio total.
            actualizarCantidadProductos() // Actualiza la cantidad de productos.
        })
    })
}

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

// Actualiza la cantidad de productos totales del carrito
function actualizarCantidadProductos() {
    let array = JSON.parse(localStorage.getItem("cartObjs")) // Trae el array de la localStorage
    let cant = 0 // Inicializa una cantidad base

    //Recorre las prendas del array
    array.forEach(function (prenda) {
        cant += parseInt(prenda.cantidad) // Suma cada cantidad por cada prenda a la variable cant
    })
    cantProds.innerHTML ="Productos: " + cant; // Setea el valor del HTML como el valor de la variable cant
}

//Actualiza el precio total del carrito
function actualizarPrecioTotal() {
    let array = JSON.parse(localStorage.getItem("cartObjs")) // Trae el array de la localStorage.
    let precioTotal = 0 // Inicializa un precio base

    //Recorre las prendas del array
    array.forEach(function (prenda) {
        precioTotal += prenda.precio * prenda.cantidad // Suma a precioTotal la multiplicacion de el precio de 
    })                                                 // la prenda con su cantidad pedida
    total.innerHTML ="Total: " + precioTotal // Setea el valor del HTML como el valor de la variable precioTotal 
}

generarHtmlCarrito();//Agrega las tarjetas al carrito.
actualizarCantidadProductos()
actualizarPrecioTotal()
