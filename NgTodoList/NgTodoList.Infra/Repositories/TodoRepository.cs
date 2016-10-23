using NgTodoList.Domain;
using NgTodoList.Domain.Repositories;
using NgTodoList.Infra.Context;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace NgTodoList.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private NgTodoListDataContext _context;

        public TodoRepository(NgTodoListDataContext context)
        {
            _context = context;
        }

        public IList<Todo> Get(string email)
        {
            var user = _context
                .Users
                .Include(x => x.Todos)
                .Where(x => x.Email.ToLower() == email.ToLower())
                .FirstOrDefault();

            if (user != null)
            {
                return user.Todos.ToList();
            }

            return new List<Todo>();
        }

        public void Sync(IList<Todo> todos, string email)
        {
            //ADO NET
            var user = _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NgTodoListConnectionString"].ConnectionString))
            {
                //abre conexão
                conn.Open();

                //limpa lista
                SqlCommand clearTodosCommand = new SqlCommand("DELETE FROM [Todo] WHERE [UserId]=@userId", conn);
                clearTodosCommand.Parameters.Add("@userId", SqlDbType.VarChar);
                clearTodosCommand.Parameters["@userId"].Value = user.Id;
                clearTodosCommand.ExecuteNonQuery();

                //recebe nova lista
                foreach (var todo in todos)
                {
                    SqlCommand insertTodosCommand = new SqlCommand("INSERT INTO [Todo] VALUES (@title, @done, @userId)", conn);

                    insertTodosCommand.Parameters.Add("@title", SqlDbType.VarChar);
                    insertTodosCommand.Parameters.Add("@done", SqlDbType.Bit);
                    insertTodosCommand.Parameters.Add("@userId", SqlDbType.Int);

                    insertTodosCommand.Parameters["@title"].Value = todo.Title;
                    insertTodosCommand.Parameters["@done"].Value = todo.Done;
                    insertTodosCommand.Parameters["@userId"].Value = user.Id;

                    insertTodosCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
