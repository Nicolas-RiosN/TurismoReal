using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Windows.Input;
using Turismo.Model;
using Turismo.Repositories;
using System.Threading;
using System.Security.Principal;

namespace Turismo.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Campos
        private string _username;
        private SecureString _password;
        private string _errorMesagge;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        //Propiedades
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMesagge 
        { 
            get
            {
                return  _errorMesagge;
            }
            set
            {
                _errorMesagge = value;
                OnPropertyChanged(nameof(ErrorMesagge));
            }
        }
        public bool IdViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IdViewVisible));
            }
        }

        //Comando
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }

        //Consructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool valideData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || 
                Password == null || Password.Length < 3)
                valideData = false;
            else
                valideData = true;
            return valideData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var IsValidUser = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (IsValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                _isViewVisible = false;
            }
            else
            {
                ErrorMesagge = "Nombre de usuario o contraseña invalidos";
            }
        }
    }
}
