using AutoMapper;
using Fituska.Server.Models.ListModels;
using Fituska.Server.Repositories;

namespace Fituska.Server.Facedes;

public class UserFacade :IFacade
{
    public readonly UserRepository userRepository;
    public readonly IMapper mapper;

    public UserFacade(UserRepository categoryRepository,
        IMapper mapper)
    {
        this.userRepository = categoryRepository;
        this.mapper = mapper;
    }

    public List<UserListModel> GetAll()
    {
        return mapper.Map<List<UserListModel>>(userRepository.GetAll());
    }
}

