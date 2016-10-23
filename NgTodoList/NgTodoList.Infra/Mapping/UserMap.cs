using NgTodoList.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NgTodoList.Infra.Mapping
{
    //mapeamento da classe User no banco de dados
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            //pk
            HasKey(x => x.Id);

            //columns
            Property(x => x.Name).IsRequired().HasMaxLength(60);
            //não pode haver dois usuários com o mesmo email, é criado um index
            Property(x => x.Email).IsRequired().HasMaxLength(160).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_USER_EMAIL") { IsUnique = true }));
            Property(x => x.Password).IsRequired().HasMaxLength(32).IsFixedLength();

            HasMany(x => x.Todos);
        }
    }
}
