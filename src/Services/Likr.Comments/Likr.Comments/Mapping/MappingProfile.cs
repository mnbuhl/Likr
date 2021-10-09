using AutoMapper;
using Likr.Comments.Dtos.v1;
using Likr.Comments.Entities;

namespace Likr.Comments.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}