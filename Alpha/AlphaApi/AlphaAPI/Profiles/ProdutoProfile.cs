using AlphaAPI.Data.DTOs;
using AlphaAPI.Data.DTOs.FakeStoreApi;
using AlphaAPI.Models;
using AutoMapper;

namespace AlphaAPI.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<UpdateProdutoDto, Produto>();
        CreateMap<Produto, UpdateProdutoDto>();
        CreateMap<Produto, GetProdutoDto>();
        CreateMap<Produto, FakeStoreProductDTO>();
    }
}