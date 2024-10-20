// Mappings/MappingProfile.cs
using AutoMapper;
using NewsAPI.DTOs;
using NewsAPI.Models;

namespace NewsAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsArticle, NewsArticleDto>();
            CreateMap<CreateNewsArticleDto, NewsArticle>();
            CreateMap<UpdateNewsArticleDto, NewsArticle>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<NewsResponse, NewsResponseDto>();
        }
    }
}
