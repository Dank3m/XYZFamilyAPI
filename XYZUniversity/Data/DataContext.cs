using Microsoft.EntityFrameworkCore;
using XYZUniversity.Models;

namespace XYZUniversity.Data;

    public class DataContext : DbContext
    {
        DbContextOptions<DataContext> _options;
        public DataContext(){
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=assessment;user=root;password="
                    ,new MySqlServerVersion(new Version(8, 0, 11)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("StudentID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);;

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stream)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => new { e.PaymentRef});

                entity.Property(e => e.Narration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankRef)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate)
                    .IsUnicode(false);
                
                entity.Property(e => e.PaymentMethod_)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentChannel_)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
                entity.Property(e => e.Amount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(s => s.Student)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(s => s.StudentId)
                    .HasConstraintName("FK_Payment_Student");

                entity.HasOne(pm => pm.PaymentMethod)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(pm => pm.PaymentMethod_)
                    .HasConstraintName("FK_Payment_PaymentMethod");

                entity.HasOne(pc => pc.PaymentChannel)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(pm => pm.PaymentChannel_)
                    .HasConstraintName("FK_Payment_PaymentChannel");
                
                entity.HasIndex(p => p.BankRef).IsUnique();
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => new { e.Method});

                entity.Property(e => e.Method)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethodDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentChannel>(entity =>
            {
                entity.HasKey(e => new { e.Channel});

                entity.Property(e => e.Channel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentChannelDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        }

        public DbSet<Student> Students { get; set;}

        public DbSet<Payment> Payments { get; set;}

        public DbSet<PaymentChannel> PaymentChannels { get; set;}

        public DbSet<PaymentMethod> PaymentMethods { get; set;}


    }

        
    
