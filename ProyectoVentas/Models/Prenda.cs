using System.ComponentModel.DataAnnotations;

namespace ProyectoVentas.Models
{
    public class Prenda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
        public int talle {  get; set; }
        public string Descripcion { get; set; }

        [EnumDataType(typeof(Publico))]
        public Publico publico { get; set; }

        [EnumDataType(typeof(Tipo))]

        public Tipo tipo { get; set; }
    }
}
