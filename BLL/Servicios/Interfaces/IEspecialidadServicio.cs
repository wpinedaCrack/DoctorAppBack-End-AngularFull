using Models.DTOs;

namespace BLL.Servicios.Interfaces
{
    public interface IEspecialidadServicio
    {
        Task<IEnumerable<EspecialidadDto>> ObtenerTodos();
        Task<IEnumerable<EspecialidadDto>> ObtenerActivos();
        Task<EspecialidadDto> Agregar(EspecialidadDto modeloDto);
        Task Actualizar(EspecialidadDto modeloDto);
        Task Remover(int id);
    }
}
