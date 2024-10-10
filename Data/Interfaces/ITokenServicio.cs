using Models.Entidades;

namespace Data.Interfaces
{
    public interface ITokenServicio
    {
        string crearToken(Usuario usuario);
    }
}
