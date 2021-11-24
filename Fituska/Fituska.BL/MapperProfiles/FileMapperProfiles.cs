using Fituska.Shared.Models.File;

namespace Fituska.BL.MapperProfiles;
public class FileMapperProfiles:Profile
{
    public FileMapperProfiles()
    {
        CreateMap<FileEntity,FileDetailModel>();
        CreateMap<FileEntity,FileListModel>();
    }
    
}   
    

