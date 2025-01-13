using Newtonsoft.Json;

namespace Inventario.Application.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
