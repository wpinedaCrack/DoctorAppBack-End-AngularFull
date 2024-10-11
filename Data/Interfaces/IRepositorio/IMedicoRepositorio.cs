using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IMedicoRepositorio : IRepositorioGenerico<Medico>
    {
        void Actualizar(Medico medico);

    }
}
