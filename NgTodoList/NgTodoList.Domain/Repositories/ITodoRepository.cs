using System;
using System.Collections.Generic;

namespace NgTodoList.Domain.Repositories
{
    public interface ITodoRepository : IDisposable
    {
        //obtém lista de todos de um determinado usuário
        //pelo email (pois é utilizado mais) 
        IList<Todo> Get(string email);
        //salvar os todos
        void Sync(IList<Todo> todos, string email);
    }
}
