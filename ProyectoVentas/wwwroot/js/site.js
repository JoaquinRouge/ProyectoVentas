let counterCart = document.getElementById("counterCart")
class Prenda {
    constructor(nombre,precio,talle,descripcion,publico) { 
        this.nombre = nombre;
        this.precio = precio;
        this.talle = talle;
        this.descripcion = descripcion;
        this.publico = publico;
    }
}
let cartObjs = []

function agregarCarrito(nombre, precio, talle, descripcion, publico) {
    cartObjs.push(new Prenda(nombre, precio, talle, descripcion, publico));
    counterCart.innerHTML = cartObjs.length;// Actualiza el contador después de agregar
}



