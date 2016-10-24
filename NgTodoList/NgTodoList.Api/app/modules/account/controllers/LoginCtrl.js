(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginCtrl', LoginCtrl);

    function LoginCtrl($rootScope, $scope, $location, AuthRepository, UserRepository) {

        //associação aos controllers (ng-model)
        $scope.login = {
            email: '',
            password: ''
        }

        $scope.authenticate = function () {
            //criando a requisição
            var promise = AuthRepository
                .getToken($scope.login.email, $scope.login.password)
                .then(
                   function (result) {
                       //salva token no localstorage
                       localStorage.setItem('token', result.data.access_token);
                       $rootScope.isAuthorized = true;
                       UserRepository.setCurrentProfile();
                       $location.path('/');
                   },
                   function (error) {
                       toastr.error(error.data.error_description, 'Falha na autenticação');
                   });
        }
    }
})();
