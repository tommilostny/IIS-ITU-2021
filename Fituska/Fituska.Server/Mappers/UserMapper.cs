using Fituska.Server.Models.DetailModels;
using Fituska.Server.Models.ListModels;

namespace Fituska.Server.Mappers;
public class UserMapper
{
    public static UserDetailModel? MapEntityToDetailModel(UserEntity entity)
    {
        if (entity == null) return null;
        var detailModel = new UserDetailModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            AttendingCourses = entity.AttendingCourses,
            DiscordUsername = entity.DiscordUsername,
            LastLoginDate = entity.LastLoginDate,
            Photo= entity.Photo, 
            PhotoID = entity.PhotoID, 
            RegistrationDate = entity.RegistrationDate,
        };
        return detailModel;
    }

    public static UserListModel? MapEntityToListModel(UserEntity entity)
    {
        if (entity == null) return null;
        var listModel = new UserListModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhotoID = entity.PhotoID,
            Photo = entity.Photo,
        };
        return listModel;
    }

    public static UserEntity MapDetailModelToEntity(UserDetailModel model)
    {
        var entity = new UserEntity
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            AttendingCourses = model.AttendingCourses,
            DiscordUsername = model.DiscordUsername,
            LastLoginDate = model.LastLoginDate,
            Photo = model.Photo,
            PhotoID = model.PhotoID,
            RegistrationDate = model.RegistrationDate,
        };
        return entity;
    }
}

