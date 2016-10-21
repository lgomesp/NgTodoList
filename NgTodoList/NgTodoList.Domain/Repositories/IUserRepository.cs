using System;
using System.Collections.Generic;

namespace NgTodoList.Domain.Repositories
{
    public interface IUserRepository : IDisposable
    {
        //obtém usuário pelo email
        User Get(string email);
        //salva as alterações de um usuário
        void SaveOrUpdate(User user);
        //Exclui usuário pelo id
        void Delete(int id);
    }
}
