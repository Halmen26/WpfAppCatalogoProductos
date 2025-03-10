namespace WpfAppCatalogProduct.Models;

public class Product
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string ImageUri { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}
