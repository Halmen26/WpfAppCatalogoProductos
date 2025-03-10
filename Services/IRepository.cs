namespace WpfAppCatalogProduct.Services
{
    public interface IRepository<T>
    {
        Task<List<T>> ObtenerAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task AgregarAsync(T entity);
        Task ActualizarAsync(T entity);
        Task EliminarAsync(int id);
    }
}
