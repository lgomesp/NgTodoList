using NgTodoList.Domain;
using System.Data.Entity.ModelConfiguration;

namespace NgTodoList.Infra.Mapping
{
    class TodoMap : EntityTypeConfiguration<Todo>
    {
        public TodoMap()
        {
            ToTable("Todo");

            HasKey(x => x.Id);

            Property(x => x.Done).IsRequired();
            Property(x => x.Title).IsRequired().HasMaxLength(160);

            //não é necessário mapear os usuários pois já foi mapeado no UserMap
        }
    }
}
