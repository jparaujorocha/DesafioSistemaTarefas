using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Application.Mappings
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaDto>()
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.idStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.status, opt => {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           })
          .ReverseMap();
        }
    }
}
