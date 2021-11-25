using Fituska.Shared.Enums;
using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.Category;
using Fituska.Shared.Models.Comment;
using Fituska.Shared.Models.Course;
using Fituska.Shared.Models.Question;
using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Vote;

namespace Fituska.Client;

internal static class DesignTimeSeed
{
    internal static List<CourseListModel> CourseListModels = new()
    {
        new CourseListModel
        {
            Shortcut = "IIS",
            Url = "iis-2021",
            Semester = Semester.Winter,
            YearOfStudy = YearOfStudy.BIT3,
        },
        new CourseListModel
        {
            Shortcut = "ITU",
            Url = "itu-2021",
            Semester = Semester.Winter,
            YearOfStudy = YearOfStudy.BIT3,
        }
    };

    internal static List<CourseDetailModel> CourseDetailModels = new()
    {
        new CourseDetailModel
        {
            Description = "IIS je předmět...",
            Name = "Informační systémy",
            Shortcut = "IIS",
            Url = "iis-2021",
            Semester = Semester.Winter,
            YearOfStudy = YearOfStudy.BIT3,
            Categories = new List<CategoryDetailModel>
            {
                new CategoryDetailModel
                {
                    Year = 2021,
                    Name = "Obecné",
                    Questions = new List<QuestionListModel>
                    {
                        new QuestionListModel
                        {
                            Id = new Guid("24640fd0-7275-4b51-9f33-18781efbeff3"),
                            Title = "Co je to PHP?",
                            User = new UserListModel
                            {
                                UserName = "xmilos02",
                            }
                        },
                        new QuestionListModel
                        {
                            Id = new Guid("4f96b07a-9758-4707-a94e-3ee884c3e1de"),
                            Title = "Pyramidové schéma, opravdu?",
                            User = new UserListModel
                            {
                                UserName = "administrator",
                            }
                        }
                    }
                },
                new CategoryDetailModel
                {
                    Year = 2021,
                    Name = "Půlsemestrální zkouška",
                    Questions = new List<QuestionListModel>
                    {
                        new QuestionListModel
                        {
                            Id = new Guid("6155a2d1-827b-44c4-bdf8-5797a142cc6b"),
                            Title = "Opravdu bude PHP v písemce?",
                            User = new UserListModel
                            {
                                UserName = "xmilos02",
                            }
                        }
                    }
                }
            }
        },
        new CourseDetailModel
        {
            Description = "ITU je předmět...",
            Name = "Tvorba uživatelských rozhraní",
            Shortcut = "ITU",
            Url = "itu-2021",
            Semester = Semester.Winter,
            YearOfStudy = YearOfStudy.BIT3,
            Categories = new List<CategoryDetailModel>
            {
                new CategoryDetailModel
                {
                    Year = 2021,
                    Name = "Obecné",
                    Questions = new List<QuestionListModel>
                    {
                        new QuestionListModel
                        {
                            Id = new Guid("5624de2c-c626-43fa-846a-300c8ab1eb83"),
                            Title = "Můžeme psát projekt v C#?",
                            User = new UserListModel
                            {
                                UserName = "administrator",
                            }
                        }
                    }
                }
            }
        }
    };

    internal static List<QuestionDetailModel> QuestionDetailModels = new()
    {
        new QuestionDetailModel
        {
            Id = new Guid("24640fd0-7275-4b51-9f33-18781efbeff3"),
            Title = "Co je to PHP?",
            User = new UserListModel
            {
                UserName = "xmilos02",
            },
            Text = "PHP vypadá jako zvláštní jazyk. Proč by to vůbec někdo používal?",
            CreationTime = new DateTime(2021, 11, 21, 9, 45, 30),
            Answers = new List<AnswerDetailModel>
            {
                new AnswerDetailModel
                {
                    Text = "No, není to zas tak hrozný, když se nad tím zamyslíš. Bývalo to celkem dobré řešení.",
                    User = new UserListModel
                    {
                        UserName = "administrator",
                    },
                    CreationTime = new DateTime(2021, 11, 21, 10, 20, 50),
                    Votes = new List<VoteModel>
                    {
                        new VoteModel { Vote = VoteValue.Downvote, User = new UserListModel { UserName = "xmilos02", Photo = null } }
                    }
                }
            }
        },
        new QuestionDetailModel
        {
            Id = new Guid("4f96b07a-9758-4707-a94e-3ee884c3e1de"),
            Title = "Pyramidové schéma, opravdu?",
            User = new UserListModel
            {
                UserName = "administrator",
            },
            Text = "Nemohu uvěřit, že něco takového zaznělo na přednášce.. Bude to na zkoušce?",
            CreationTime = new DateTime(2021, 10, 19, 15, 36, 0),
            Answers = new List<AnswerDetailModel>()
        },
        new QuestionDetailModel
        {
            Id = new Guid("6155a2d1-827b-44c4-bdf8-5797a142cc6b"),
            Title = "Opravdu bude PHP v písemce?",
            User = new UserListModel
            {
                UserName = "xmilos02",
            },
            Text = "Když můžeme dělat projekt v jakémkoliv jazyce, proč je PHP vyžadováno? Jak wtf.. :D",
            CreationTime = new DateTime(2021, 9, 29, 16, 25, 1),
            Answers = new List<AnswerDetailModel>
            {
                new AnswerDetailModel
                {
                    Text = "Je asi dobrý mít nějaký společný jazyk pro ukázku příkladů.",
                    CreationTime = new DateTime(2021, 9, 29, 16, 27, 23),
                    User = new UserListModel
                    {
                        UserName = "administrator",
                    },
                    Comments = new List<CommentDetailModel>
                    {
                        new CommentDetailModel
                        {
                            Text = "Ok, to dává smysl. Ale proč zrovan PHP?",
                            User = new UserListModel
                            {
                                UserName = "xmilos02",
                            },
                            CreationTime = new DateTime(2021, 9, 29, 17, 0, 0),
                            SubComments = new List<CommentDetailModel>
                            {
                                new CommentDetailModel
                                {
                                    Text = "Idk, ale stále hodně se používá.",
                                    User = new UserListModel
                                    {
                                        UserName = "administrator",
                                    },
                                    CreationTime = new DateTime(2021, 9, 30, 14, 14, 14)
                                }
                            }
                        }
                    }
                }
            }
        },
        new QuestionDetailModel
        {
            Id = new Guid("5624de2c-c626-43fa-846a-300c8ab1eb83"),
            Title = "Můžeme psát projekt v C#?",
            User = new UserListModel
            {
                UserName = "administrator",
            },
            Text = "Jako v tomto předmětu to vypadá, že je to jedno, ale zajímá mě váš názor.",
            CreationTime = new DateTime(2021, 9, 27, 17, 1, 58),
            Answers = new List<AnswerDetailModel>
            {
                new AnswerDetailModel
                {
                    Text = "Jo, můžeme si dělat, co chceme.",
                    CreationTime = new DateTime(2021, 9, 27, 23, 30, 30),
                    User = new UserListModel
                    {
                        UserName = "xmilos02",
                    },
                    Votes = new List<VoteModel>
                    {
                        new VoteModel
                        {
                            Vote = VoteValue.Upvote,
                            User = new UserListModel
                            {
                                UserName = "administrator",
                            }
                        },
                        new VoteModel
                        {
                            Vote = VoteValue.Upvote,
                            User = new UserListModel
                            {
                                UserName = "xmilos02"
                            }
                        }
                    },
                    Comments = new List<CommentDetailModel>
                    {
                        new CommentDetailModel
                        {
                            Text = "Ok, díky.",
                            User = new UserListModel
                            {
                                UserName = "administrator"
                            },
                            CreationTime = new DateTime(2021, 9, 27, 23, 59, 0)
                        }
                    }
                }
            }
        }
    };
}
