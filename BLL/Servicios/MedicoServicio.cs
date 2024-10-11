using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;

namespace BLL.Servicios
{
    public class MedicoServicio : IMedicoServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public MedicoServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task<MedicoDto> Agregar(MedicoDto modeloDto)
        {
            try
            {
                Medico nuevoMedico = new Medico
                {
                    Nombres = modeloDto.Nombres,
                    Apellidos = modeloDto.Apellidos,
                    Telefono = modeloDto.Telefono,
                    Direccion = modeloDto.Direccion,
                    Genero = modeloDto.Genero,
                    EspecialidadId = modeloDto.EspecialidadId,
                    Estado = modeloDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                await _unidadTrabajo.Medico.Agregar(nuevoMedico);
                await _unidadTrabajo.Guardar();
                if (nuevoMedico.Id == 0)
                    throw new TaskCanceledException("La Medico no se puede crear");

                return _mapper.Map<MedicoDto>(nuevoMedico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Actualizar(MedicoDto modeloDto)
        {
            try
            {
                var medicoDB = await _unidadTrabajo.Medico.ObtenerPrimero(x => x.Id == modeloDto.Id);
                if (medicoDB == null)
                    throw new TaskCanceledException("La Medico no existe");

                medicoDB.Nombres = modeloDto.Nombres;
                medicoDB.Apellidos = modeloDto.Apellidos;
                medicoDB.Direccion = modeloDto.Direccion;
                medicoDB.Telefono = modeloDto.Telefono;
                medicoDB.Genero = modeloDto.Genero;
                medicoDB.FechaActualizacion = DateTime.Now;
                medicoDB.Estado = modeloDto.Estado == 1 ? true : false;

                _unidadTrabajo.Medico.Actualizar(medicoDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<MedicoDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Medico.ObtenerTodos(incluirPropiedades: "Especialidad",//wpineda revisar
                    orderBy: e => e.OrderBy(e => e.Nombres));

                return _mapper.Map<IEnumerable<MedicoDto>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var medicoDB = await _unidadTrabajo.Medico.ObtenerPrimero(x => x.Id == id);
                if (medicoDB == null)
                    throw new TaskCanceledException("La Medico no existe");

                _unidadTrabajo.Medico.Remover(medicoDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}