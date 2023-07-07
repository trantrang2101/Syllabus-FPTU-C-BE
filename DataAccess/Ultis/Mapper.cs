using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Ultis
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<AccountRole, AccountRoleDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<RoleSidebar, RoleSidebarDTO>().ReverseMap();

            CreateMap<Sidebar, SidebarDTO>().ReverseMap();

            CreateMap<Account,AccountDTO>().ForMember(des => des.Roles,
                act => act.MapFrom(o => o.AccountRoles.Select(ar => ar.Role)));
            CreateMap<AccountDTO,Account>();

            CreateMap<Assessment, AssessmentDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Class, ClassDTO>().ReverseMap();

            CreateMap<Combo, ComboDTO>().ReverseMap();

            CreateMap<ComboCurriculum, ComboCurriculumDTO>().ReverseMap();

            CreateMap<ComboDetail, ComboDetailDTO>().ReverseMap();

            CreateMap<Course, CourseDTO>().ReverseMap();

            CreateMap<Curriculum, CurriculumDTO>().ReverseMap();

            CreateMap<CurriculumDetail, CurriculumDetailDTO>().ReverseMap();

            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<GradeDetail, GradeDetailDTO>().ReverseMap();

            CreateMap<GradeGeneral, GradeGeneralDTO>().ReverseMap();

            CreateMap<Major, MajorDTO>().ReverseMap();

            CreateMap<StudentCourse, StudentCourseDTO>().ReverseMap();

            CreateMap<StudentProgress, StudentProgressDTO>().ReverseMap();

            CreateMap<Subject, SubjectDTO>().ReverseMap();

            CreateMap<Term, TermDTO>().ReverseMap();
        }

        protected internal Mapper(string profileName) : base(profileName) { }
    }
}
