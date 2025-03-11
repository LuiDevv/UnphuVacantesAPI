using AutoMapper;
using api.Models;
using api.Dtos;
using System;


namespace api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<UpdateCommentRequest, Comment>();

            // User <-> UserDTO
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CreateUserRequestDTO, User>();

            // Company <-> CompanyDTO
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CreateCompanyRequestDTO, Company>();

            // Job <-> JobDTO
            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.JobTypeId, opt => opt.MapFrom(src => src.JobTypeId))
                .ForMember(dest => dest.JobCategoryId, opt => opt.MapFrom(src => src.JobCategoryId))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ReverseMap();
            CreateMap<CreateJobRequestDTO, Job>();

            // Notification <-> NotificationDTO
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();
            CreateMap<CreateNotificationRequest, Notification>();

            // JobApplication <-> JobApplicationDTO
            CreateMap<JobApplication, JobApplicationDTO>().ReverseMap();
            CreateMap<CreateJobApplicationRequest, JobApplication>();

            // Role <-> RoleDTO
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<CreateRoleRequest, Role>();

            // Request DTOs (Para creación de objetos desde API)
            CreateMap<CreateUserRequestDTO, User>();
            CreateMap<CreateCompanyRequestDTO, Company>();
            CreateMap<CreateJobRequestDTO, Job>();
            CreateMap<CreateNotificationRequest, Notification>();
            CreateMap<CreateRoleRequest, Role>();

            // Update DTOs
            CreateMap<UpdateUserRequest, User>();
            CreateMap<UpdateCompanyRequest, Company>();
            CreateMap<UpdateJobRequest, Job>();
            CreateMap<UpdateNotificationRequest, Notification>();
            CreateMap<UpdateRoleRequest, Role>();
        }
    }
}
