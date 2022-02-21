using Microsoft.EntityFrameworkCore;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advisor> Advisors { get; set; } = null!;
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentClass> StudentClasses { get; set; } = null!;
        public virtual DbSet<Take> Takes { get; set; } = null!;
        public virtual DbSet<Teach> Teaches { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost;database=UniversitySmall;user id=sa;Password=ConstraintDB123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advisor>(entity =>
            {
                entity.HasKey(e => e.SId)
                    .HasName("PK__advisor__2F3DA3BC2AECBF6B");

                entity.ToTable("advisor");

                entity.Property(e => e.SId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("s_ID");

                entity.Property(e => e.IId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("i_ID");

                entity.HasOne(d => d.IIdNavigation)
                    .WithMany(p => p.Advisors)
                    .HasForeignKey(d => d.IId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__advisor__i_ID__4222D4EF");

                entity.HasOne(d => d.SIdNavigation)
                    .WithOne(p => p.Advisor)
                    .HasForeignKey<Advisor>(d => d.SId)
                    .HasConstraintName("FK__advisor__s_ID__4316F928");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(e => new { e.Building, e.RoomNumber })
                    .HasName("PK__classroo__9FD2687A24C09D57");

                entity.ToTable("classroom");

                entity.Property(e => e.Building)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("building");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("room_number");

                entity.Property(e => e.Capacity)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("capacity");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.Credits)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("credits");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dept_name");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.DeptNameNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DeptName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__course__dept_nam__2A4B4B5E");

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Prereqs)
                    .UsingEntity<Dictionary<string, object>>(
                        "Prereq",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("FK__prereq__course_i__4BAC3F29"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("PrereqId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prereq__prereq_i__4CA06362"),
                        j =>
                        {
                            j.HasKey("CourseId", "PrereqId").HasName("PK__prereq__1FEC8F07DFA6D67B");

                            j.ToTable("prereq");

                            j.IndexerProperty<string>("CourseId").HasMaxLength(8).IsUnicode(false).HasColumnName("course_id");

                            j.IndexerProperty<string>("PrereqId").HasMaxLength(8).IsUnicode(false).HasColumnName("prereq_id");
                        });

                entity.HasMany(d => d.Prereqs)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "Prereq",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("PrereqId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prereq__prereq_i__4CA06362"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("FK__prereq__course_i__4BAC3F29"),
                        j =>
                        {
                            j.HasKey("CourseId", "PrereqId").HasName("PK__prereq__1FEC8F07DFA6D67B");

                            j.ToTable("prereq");

                            j.IndexerProperty<string>("CourseId").HasMaxLength(8).IsUnicode(false).HasColumnName("course_id");

                            j.IndexerProperty<string>("PrereqId").HasMaxLength(8).IsUnicode(false).HasColumnName("prereq_id");
                        });
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptName)
                    .HasName("PK__departme__C7D39AE0FCB25269");

                entity.ToTable("department");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dept_name");

                entity.Property(e => e.Budget)
                    .HasColumnType("numeric(12, 2)")
                    .HasColumnName("budget");

                entity.Property(e => e.Building)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("building");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("instructor");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dept_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Salary)
                    .HasColumnType("numeric(8, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.DeptNameNavigation)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.DeptName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__instructo__dept___2E1BDC42");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.SecId, e.Semester, e.Year })
                    .HasName("PK__section__2B3A9AD867A9D66E");

                entity.ToTable("section");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.SecId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sec_id");

                entity.Property(e => e.Semester)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semester");

                entity.Property(e => e.Year)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("year");

                entity.Property(e => e.Building)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("building");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("room_number");

                entity.Property(e => e.TimeSlotId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("time_slot_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__section__course___32E0915F");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => new { d.Building, d.RoomNumber })
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__section__33D4B598");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dept_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TotCred)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("tot_cred");

                entity.HasOne(d => d.DeptNameNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DeptName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__student__dept_na__3B75D760");
            });

            modelBuilder.Entity<StudentClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("StudentClasses");

                entity.Property(e => e.Building)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("building");

                entity.Property(e => e.CourseDept)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("course_dept");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.Coursecreds)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("coursecreds");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("grade");

                entity.Property(e => e.Iid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("iid");

                entity.Property(e => e.Iname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("iname");

                entity.Property(e => e.Major)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("major");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("room_number");

                entity.Property(e => e.SecId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sec_id");

                entity.Property(e => e.Semester)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semester");

                entity.Property(e => e.Sid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sid");

                entity.Property(e => e.Sname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sname");

                entity.Property(e => e.TimeSlotId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("time_slot_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.TotCred)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("tot_cred");

                entity.Property(e => e.Year)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Take>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CourseId, e.SecId, e.Semester, e.Year })
                    .HasName("PK__takes__A0A7458AC8FBF546");

                entity.ToTable("takes");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.SecId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sec_id");

                entity.Property(e => e.Semester)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semester");

                entity.Property(e => e.Year)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("year");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("grade");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__takes__ID__3F466844");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => new { d.CourseId, d.SecId, d.Semester, d.Year })
                    .HasConstraintName("FK__takes__3E52440B");
            });

            modelBuilder.Entity<Teach>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CourseId, e.SecId, e.Semester, e.Year })
                    .HasName("PK__teaches__A0A7458AF90D54AF");

                entity.ToTable("teaches");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.SecId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sec_id");

                entity.Property(e => e.Semester)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semester");

                entity.Property(e => e.Year)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("year");

                entity.Property(e => e.TeacherCredit)
                    .HasColumnType("numeric(3, 2)")
                    .HasColumnName("teacher_credit");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Teaches)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__teaches__ID__37A5467C");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Teaches)
                    .HasForeignKey(d => new { d.CourseId, d.SecId, d.Semester, d.Year })
                    .HasConstraintName("FK__teaches__36B12243");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(e => new { e.TimeSlotId, e.Day, e.StartHr, e.StartMin })
                    .HasName("PK__time_slo__E86139107B93C4E3");

                entity.ToTable("time_slot");

                entity.Property(e => e.TimeSlotId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("time_slot_id");

                entity.Property(e => e.Day)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("day");

                entity.Property(e => e.StartHr)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("start_hr");

                entity.Property(e => e.StartMin)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("start_min");

                entity.Property(e => e.EndHr)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("end_hr");

                entity.Property(e => e.EndMin)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("end_min");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
