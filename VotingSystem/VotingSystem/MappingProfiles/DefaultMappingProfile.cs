using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.Models;

namespace VotingSystem.MappingProfiles
{
    public class DefaultMappingProfile : AutoMapper.Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<Project, ProjectReadDto>()
                .ForMember(
                    dto => dto.EstimatedTimeInDays, 
                    mapping => mapping.MapFrom(p => p.EstimatedRealizationTime.Value.TotalDays))
                .ForMember(
                    dto => dto.PricePln,
                    mapping => mapping.MapFrom(p => p.Price.PLN))
                .ForMember(
                    dto => dto.District,
                    mapping => mapping.MapFrom(p => p.District.Name))
                .ForMember(
                    dto => dto.TheEditionThisProjectWonId,
                    mapping => mapping.MapFrom(p => p.ConcludedEditionId))
                .ForMember(
                    dto => dto.TheEditionThisProjectWonDescription,
                    mapping => mapping.MapFrom(p => p.ConcludedEdition != null ? p.ConcludedEdition.Description : null));

            CreateMap<ProjectWriteDto, Project>()
                .ForMember(
                    p => p.EstimatedRealizationTime,
                    mapping => mapping.MapFrom(dto => new Duration(TimeSpan.FromDays(dto.EstimatedTimeInDays))))
                .ForMember(
                    p => p.Price,
                    mapping => mapping.MapFrom(dto => new Price(dto.PricePln)));

            CreateMap<EditionDraft, EditionDraftReadDto>()
                .ForMember(dto => dto.District, mapping => mapping.MapFrom(e => e.District.Name));

            CreateMap<EditionDraftReadDto, EditionDraft>();
            CreateMap<EditionDraftWriteDto, EditionDraft>();

            CreateMap<Edition, EditionReadDto>()
                .ForMember(dto => dto.District, mapping => mapping.MapFrom(e => e.District.Name));
            
            CreateMap<EditionParticipant, EditionParticipantReadDto>()
                .ForMember(dto => dto.Project, mapping => mapping.MapFrom(model => model.Project))
                .ForMember(dto => dto.VoteCount, mapping => mapping.MapFrom(model => model.Votes.Count));

            CreateMap<User, UserReadDto>();

            CreateMap<District, DistrictReadDto>();

            CreateMap<Subscriber, SubscriberReadDto>();
            CreateMap<SubscriberRegisterDto, Subscriber>()
                .ConstructUsing(dto => Subscriber.WithEmail(dto.Email));
        }
    }
}
