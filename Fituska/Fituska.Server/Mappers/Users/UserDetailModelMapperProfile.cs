using Fituska.Server.Models.DetailModels;
using Fituska.Server.Models.ListModels;
using AutoMapper;

namespace Fituska.Server.Mappers;
public class UserDetailModelMapperProfile : Profile
{
    public UserDetailModelMapperProfile()
    {
        CreateMap<UserEntity, UserDetailModel>();
    }
}

