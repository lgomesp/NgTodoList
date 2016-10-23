using NgTodoList.Domain;
using NgTodoList.Infra.Mapping;
using System.Data.Entity;

namespace NgTodoList.Infra.Context
{
    public class NgTodoListDataContext : DbContext
    {
        public NgTodoListDataContext()
            :base("NgTodoListConnectionString")
        {
            //toda vez que houver alteração em objetos ele altera modificações no banco
            //nível de desenvolvimento
            //Database.SetInitializer < NgTodoListDataContext>
            //    (new NgTodoListDataContextInitializer());

            //não é carregado nada sobre demanda
            //só carrega algo quando solicitado
            Configuration.LazyLoadingEnabled = false;
            //proxy desabilitado pois dificultará a serialização futuramente
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TodoMap());
        }
    }

    //importante durante o desenvolvimento (pois há muitas modificações no banco)
    //public class NgTodoListDataContextInitializer : DropCreateDatabaseIfModelChanges<NgTodoListDataContext>
    //{
    //    protected override void Seed(NgTodoListDataContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}
