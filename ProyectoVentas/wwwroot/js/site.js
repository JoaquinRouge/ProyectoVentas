class Prenda {
    constructor(id, nombre, precio, stock, talle, descripcion) { 
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.stock = stock;
        this.talle = talle;
        this.descripcion = descripcion;
    }

    agregar(id, nombre, precio, stock, talle, descripcion) {
        cartObjs.push(new Prenda(id,nombre,precio,stock,talle,descripcion))
    }
}
let cartObjs = []

let counterCart = document.getElementById("counterCart")

counterCart.innerHTML = cartObjs.length


