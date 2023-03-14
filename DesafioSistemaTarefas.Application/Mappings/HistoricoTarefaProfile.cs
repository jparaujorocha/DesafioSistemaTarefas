using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Application.Mappings
{
    public class HistoricoTarefaProfile : Profile
    {
        public HistoricoTarefaProfile()
        {
            CreateMap<Tarefa, HistoricoTarefaDto>()
           .ForMember(dest => dest.id, opt =>
           {
               opt.UseDestinationValue()
               ;
           })
           .ForSourceMember(src => src.Id, opt =>
           {
               opt.DoNotValidate();
           })
           .ForMember(dest => dest.IdTarefa, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.idStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.status, opt =>
           {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           }).ReverseMap();


            CreateMap<HistoricoTarefa, HistoricoTarefaDto>()
           .ForMember(dest => dest.idStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.status, opt =>
           {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           }).ReverseMap();
        }
    }
}
