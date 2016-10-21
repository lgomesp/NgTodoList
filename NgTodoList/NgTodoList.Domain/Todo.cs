using System;

namespace NgTodoList.Domain
{
    public class Todo
    {
        protected Todo() { }

        //caso não informe o id do usuário ele atribui 0 - sobrecarga
        public Todo(string title)
            : this(title, 0)
        { }

        public Todo(string title, int userId)
        {
            if (title.Length < 3)
                throw new Exception("Título inválido");

            this.Id = 0;
            this.Title = title;
            //inicia tarefa como não concluída
            this.Done = false;
            this.UserId = userId;
        }

        //Propriedades
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        //Tarefa realizada
        public bool Done { get; protected set; }
        //Relacionamento com o User
        public int UserId { get; protected set; }

        //Métodos
        public void MarkAsDone()
        {
            this.Done = true;
        }

        public void MarkAsUndone()
        {
            this.Done = false;
        }
    }
}
