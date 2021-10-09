using AutoMapper;
using Likr.Posts.Dtos.v1;
using Likr.Posts.Entities;

namespace Likr.Posts.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, PostDto>();
        }
    }
}