using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;

namespace CRMWeb.Api.Mappings
{
    public class CRMProfile : Profile
    {
        public CRMProfile() 
        {
            CreateMap<ClienteRequest, Cliente>();

            CreateMap<Cliente, ClienteResponse>();

            CreateMap<EnderecoRequest, Endereco>();

            CreateMap<Endereco, EnderecoResponse>();
        }
    }
}
