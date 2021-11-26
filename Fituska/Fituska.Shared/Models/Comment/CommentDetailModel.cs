﻿using Fituska.Shared.Models.File;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Comment;

public record CommentDetailModel : ModelBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public UserListModel User { get; set; }
    public List<CommentDetailModel> SubComments { get; set; } = new();
}