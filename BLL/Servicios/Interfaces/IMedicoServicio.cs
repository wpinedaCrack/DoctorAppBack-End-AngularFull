using Models.DTOs;

namespace BLL.Servicios.Interfaces
{
    public interface IMedicoServicio
    {
        Task<IEnumerable<MedicoDto>> ObtenerTodos();
        Task<MedicoDto> Agregar(MedicoDto modeloDto);
        Task Actualizar(MedicoDto modeloDto);
        Task Remover(int id);
    }
}
