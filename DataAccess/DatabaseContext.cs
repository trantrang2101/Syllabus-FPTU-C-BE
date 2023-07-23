using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountRole> AccountRoles { get; set; } = null!;
        public virtual DbSet<Assessment> Assessments { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Combo> Combos { get; set; } = null!;
        public virtual DbSet<ComboCurriculum> ComboCurricula { get; set; } = null!;
        public virtual DbSet<ComboDetail> ComboDetails { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Curriculum> Curricula { get; set; } = null!;
        public virtual DbSet<CurriculumDetail> CurriculumDetails { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<GradeDetail> GradeDetails { get; set; } = null!;
        public virtual DbSet<GradeGeneral> GradeGenerals { get; set; } = null!;
        public virtual DbSet<Major> Majors { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleSidebar> RoleSidebars { get; set; } = null!;
        public virtual DbSet<Sidebar> Sidebars { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<StudentProgress> StudentProgresses { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("Database"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccountRole>(entity =>
            {
                entity.ToTable("account_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__account_r__accou__245D67DE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__account_r__role___25518C17");
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("assessment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__assessmen__categ__2645B050");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Combo>(entity =>
            {
                entity.ToTable("combo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ComboCurriculum>(entity =>
            {
                entity.ToTable("combo_curriculum");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.CurriculumId).HasColumnName("curriculum_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Semester)
                    .HasColumnName("semester")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboCurricula)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__combo_cur__combo__2739D489");

                entity.HasOne(d => d.Curriculum)
                    .WithMany(p => p.ComboCurricula)
                    .HasForeignKey(d => d.CurriculumId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__combo_cur__curri__282DF8C2");
            });

            modelBuilder.Entity<ComboDetail>(entity =>
            {
                entity.ToTable("combo_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ReplaceSubjectId).HasColumnName("replace_subject_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__combo_det__combo__29221CFB");

                entity.HasOne(d => d.ReplaceSubject)
                    .WithMany(p => p.ComboDetailReplaceSubjects)
                    .HasForeignKey(d => d.ReplaceSubjectId)
                    .HasConstraintName("FK__combo_det__repla__2A164134");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ComboDetailSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__combo_det__subje__2B0A656D");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.Property(e => e.TermId).HasColumnName("term_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__course__class_id__2BFE89A6");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__course__subject___2CF2ADDF");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__course__teacher___2DE6D218");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__course__term_id__2EDAF651");
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.ToTable("curriculum");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Curricula)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__curriculu__major__2FCF1A8A");
            });

            modelBuilder.Entity<CurriculumDetail>(entity =>
            {
                entity.ToTable("curriculum_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.CurriculumId).HasColumnName("curriculum_id");

                entity.Property(e => e.MinMark)
                    .HasColumnName("min_mark")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Curriculum)
                    .WithMany(p => p.CurriculumDetails)
                    .HasForeignKey(d => d.CurriculumId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__curriculu__curri__30C33EC3");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.CurriculumDetails)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__curriculu__subje__31B762FC");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GradeDetail>(entity =>
            {
                entity.ToTable("grade_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .HasColumnName("comment");

                entity.Property(e => e.GradeGeneralId).HasColumnName("grade_general_id");

                entity.Property(e => e.InsertedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("inserted_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StudentCourseId).HasColumnName("student_course_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.GradeGeneral)
                    .WithMany(p => p.GradeDetails)
                    .HasForeignKey(d => d.GradeGeneralId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__grade_det__grade__32AB8735");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.GradeDetails)
                    .HasForeignKey(d => d.StudentCourseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__grade_det__stude__339FAB6E");
            });

            modelBuilder.Entity<GradeGeneral>(entity =>
            {
                entity.ToTable("grade_general");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssessmentId).HasColumnName("assessment_id");

                entity.Property(e => e.CurriculumDetailId).HasColumnName("curriculum_detail_id");

                entity.Property(e => e.MinMark)
                    .HasColumnName("min_mark")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.GradeGenerals)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__grade_gen__asses__3493CFA7");

                entity.HasOne(d => d.CurriculumDetail)
                    .WithMany(p => p.GradeGenerals)
                    .HasForeignKey(d => d.CurriculumDetailId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__grade_gen__curri__3587F3E0");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("major");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__major__parent_id__367C1819");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RoleSidebar>(entity =>
            {
                entity.ToTable("role_sidebar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.SidebarId).HasColumnName("sidebar_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleSidebars)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__role_side__role___44CA3770");

                entity.HasOne(d => d.Sidebar)
                    .WithMany(p => p.RoleSidebars)
                    .HasForeignKey(d => d.SidebarId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__role_side__sideb__45BE5BA9");
            });

            modelBuilder.Entity<Sidebar>(entity =>
            {
                entity.ToTable("sidebar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__sidebar__parent___395884C4");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("student_course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student_c__cours__3A4CA8FD");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student_c__stude__3B40CD36");
            });

            modelBuilder.Entity<StudentProgress>(entity =>
            {
                entity.ToTable("student_progress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurriculumId).HasColumnName("curriculum_id");

                entity.Property(e => e.CurriculumStatus)
                    .HasColumnName("curriculum_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TermId).HasColumnName("term_id");

                entity.Property(e => e.TermStatus)
                    .HasColumnName("term_status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Curriculum)
                    .WithMany(p => p.StudentProgresses)
                    .HasForeignKey(d => d.CurriculumId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student_p__curri__3C34F16F");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentProgresses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student_p__stude__3D2915A8");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.StudentProgresses)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student_p__term___3E1D39E1");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Slot).HasColumnName("slot");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__subject__departm__3F115E1A");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("term");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
