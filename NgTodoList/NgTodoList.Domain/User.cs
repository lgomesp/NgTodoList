using NgTodoList.Utils.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace NgTodoList.Domain
{
    public class User
    {
        //classe vazia apenas para uso no entityframework (constructor parameterless)
        protected User() { }

        public User(string name, string email, string password)
        {
            if (name.Length < 3)
                throw new Exception("Nome inválido");
            //code contracts vs
            //requer que o nome seja MAIOR que três, senão > exceção
            //Contract.Requires<Exception>(name.Length > 3, "Nome Inválido");

            //Regular expressions
            if(!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$"))
                throw new Exception("Email inválido");

            if (password.Length < 6)
                throw new Exception("Senha inválida");

            //o construtor atual inicia o user com id 0
            this.Id = 0;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsActive = true;
            this._todos = new List<Todo>();
            this.Todos = new List<Todo>();
        }

        //Lista de todos para ser trabalhadas internamente
        private IList<Todo> _todos;

        #region Propriedades
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        //Status do usuário
        public bool IsActive { get; set; }
        //Restringe o acesso aos modificadores
        public virtual ICollection<Todo> Todos
        {
            get { return _todos; }
            protected set { _todos = new List<Todo>(value); }
        }
        #endregion

        #region Métodos
        public void ChangePassword(string email, string password, string newPassword, string confirmPassword)
        {
            if (!Regex.IsMatch(email, @"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}"))
                throw new Exception("Email inválido");

            if (password.Length < 6)
                throw new Exception("Senha inválida");

            if (!(this.Email.ToLower() == email.ToLower()) && !(this.Password == password))
                throw new Exception("Usuário ou senha inválidos");

            if (newPassword.Length < 6)
                throw new Exception("Nova senha inválida");

            if(newPassword!=confirmPassword)
                throw new Exception("As senhas não conferem");

            //encriptando a senha para não salva-la direto no banco
            var pass = EncryptHelper.Encrypt(newPassword);
            this.Password = pass;
        }

        public string ResetPassword(string email)
        {
            Contract.Requires<Exception>(this.Email.ToLower() == email.ToLower(), "Usuário inválido");

            var password = System.Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = EncryptHelper.Encrypt(password);

            return password;
        }

        public void Authenticate(string email, string password)
        {
            if (!IsActive)
                throw new Exception("Este usuário está inativo");

            if (!Regex.IsMatch(email, @"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}"))
                throw new Exception("Email inválido");

            var pass = EncryptHelper.Encrypt(password);

            if(!(this.Email.ToLower() == email.ToLower()) && !(this.Password == pass))
                throw new Exception("Usuário ou senha inválidos");
        }

        public void UpdateInfo(string name)
        {
            if (name.Length < 3)
                throw new Exception("Nome inválido");

            this.Name = name;
        }

        //public void SyncTodos(IList<Todo> todos)
        //{
        //    Contract.Requires<Exception>(todos != null, "Lista de tarefas inválida");

        //    this._todos = new List<Todo>();

        //    foreach (var item in todos)
        //    {
        //        var todo = new Todo(item.Title, this.Id);
        //        this._todos.Add(todo);
        //    }
        //}

        public void ClearTodos()
        {
            this._todos = new List<Todo>();
        }

        public void Inactivate()
        {
            this.IsActive = false;
        }

        public void Activate()
        {
            this.IsActive = true;
        }
        #endregion
    }
}
