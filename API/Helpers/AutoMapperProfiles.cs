using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    /// <summary>
    /// Contains AutoMapper profiles for mapping entities to DTOs and vice versa.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Initializes AutoMapper mapping configurations.
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));
            CreateMap<Message, MessageDto>();
            CreateMap<User, UserClientDto>();
            CreateMap<Comment, CommentResponseDto>();
            CreateMap<Follow, FollowDto>();
            CreateMap<FollowDto, Follow>();
            CreateMap<UserClientDto,User>();
        }

    }
}