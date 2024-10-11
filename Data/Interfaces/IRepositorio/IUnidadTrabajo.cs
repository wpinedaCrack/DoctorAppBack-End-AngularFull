namespace Data.Interfaces.IRepositorio
{
    public interface IUnidadTrabajo: IDisposable//patron IUnitofWork
    {
        IEspecialidadRepositorio Especialidad { get; }
        IMedicoRepositorio Medico { get; }
        Task Guardar();
    }
}
