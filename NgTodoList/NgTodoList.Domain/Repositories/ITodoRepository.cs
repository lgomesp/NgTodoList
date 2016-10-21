using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgTodoList.Domain.Repositories
{
    public interface ITodoRepository : IDisposable
    {
        //obtém lista de todos de um determinado usuário
        //pelo email (pois é utilizado mais 
        IList<Todo> Get(string email);
    }
}
