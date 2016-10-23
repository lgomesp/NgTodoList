using NgTodoList.Domain;
using NgTodoList.Domain.Repositories;
using NgTodoList.Infra.Context;
using System.Linq;

namespace NgTodoList.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private NgTodoListDataContext _context;

        public UserRepository(NgTodoListDataContext context)
        {
            _context = context;
        }

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public void SaveOrUpdate(User user)
        {
            //novo usuário
            if(user.Id == 0)
            {
                _context.Users.Add(user);
            } else //atualização
            {
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));
        }

        public void Dispose()
        {
            _context.Dispose();   
        }
    }
}
