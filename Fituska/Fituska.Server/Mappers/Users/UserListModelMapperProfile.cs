using Fituska.Server.Models.DetailModels;
using Fituska.Server.Models.ListModels;
using AutoMapper;

namespace Fituska.Server.Mappers;
public class UserListModelMapperProfile : Profile
{
    public UserListModelMapperProfile()
    {
        CreateMap<UserEntity, UserListModel>();
    }
}

