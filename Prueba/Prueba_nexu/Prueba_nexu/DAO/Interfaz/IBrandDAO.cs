using Prueba_nexu.DAO.Modelo;

namespace Prueba_nexu.DAO.Interfaz
{
    public interface IBrandDAO
    {
        public  Task<List<brand>> ObtenerTodasMarcas();
        public Task<brand> AgregarMarca(brand Marca);
    }
}
