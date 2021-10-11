using AutoMapper;
using Likr.Likes.Dtos.v1;
using Likr.Likes.Entities;

namespace Likr.Likes.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLikeDto, Like>();
            CreateMap<Like, LikeDto>();
        }
    }
}