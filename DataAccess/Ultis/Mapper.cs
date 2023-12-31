﻿using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;

namespace DataAccess.Ultis
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<AccountRole, AccountRoleDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ForMember(des => des.Sidebars,
                act => act.MapFrom(o => o.RoleSidebars.Select(ar => ar.Sidebar)));
            CreateMap<RoleDTO, Role>();

            CreateMap<RoleSidebar, RoleSidebarDTO>().ReverseMap();

            CreateMap<SidebarDTO,Sidebar>().ForMember(des => des.ParentId,
                act => act.MapFrom(o => o.Parent.Id)).ReverseMap();

            CreateMap<Account, AccountDTO>().ForMember(des => des.Roles,
                act => act.MapFrom(o => o.AccountRoles.Select(ar => ar.Role)))
                .ForMember(des => des.Sidebars,
                act => act.MapFrom(o => o.AccountRoles.SelectMany(ar => ar.Role.RoleSidebars.Select(rs=>rs.Sidebar))));
            CreateMap<AccountDTO,Account>();

            CreateMap<AssessmentDTO,Assessment>().ForMember(des => des.CategoryId,
                act => act.MapFrom(o => o.Category.Id)).ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Class, ClassDTO>().ReverseMap();

            CreateMap<Course, CourseDTO>().ReverseMap();

            CreateMap<Curriculum, CurriculumDTO>().ReverseMap();

            CreateMap<CurriculumDetail, CurriculumDetailDTO>().ReverseMap();

            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<GradeDetail, GradeDetailDTO>().ReverseMap();

            CreateMap<GradeGeneral, GradeGeneralDTO>().ReverseMap();

            CreateMap<Major, MajorDTO>().ReverseMap();

            CreateMap<StudentCourse, StudentCourseDTO>().ReverseMap();

            CreateMap<StudentProgress, StudentProgressDTO>().ReverseMap();

            CreateMap<SubjectDTO,Subject>().ForMember(des => des.DepartmentId,
                act => act.MapFrom(o => o.Department.Id)).ReverseMap();

            CreateMap<Term, TermDTO>().ReverseMap();
        }

        protected internal Mapper(string profileName) : base(profileName) { }
    }
}
