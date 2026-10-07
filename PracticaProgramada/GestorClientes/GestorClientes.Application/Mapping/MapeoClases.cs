using AutoMapper;
using GestorClientes.Application.Dtos;
using GestorClientes.Domain;

namespace GestorClientes.Application.Mapping
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Telefono, TelefonoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}
