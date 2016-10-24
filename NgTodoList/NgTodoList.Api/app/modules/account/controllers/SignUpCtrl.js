(function () {
    'use strict';

    angular
        .module('app')
        .controller('SignUpCtrl', SignUpCtrl);

    SignUpCtrl.$inject = ['$scope', 'UserRepository', '$location'];

    function SignUpCtrl($scope, UserRepository, $location) {
        //registeruserviewmodel
        $scope.signupModel = {
            name: '',
            email: '',
            password: '',
            confirmPassword: ''
        };

        $scope.signup = function ($event) {
            console.log($event);
            UserRepository.register($scope.signupModel).then(
                function (result) {
                    toastr.success(result, 'Cadastro efetuado com sucesso');
                    $location.path('/signup-success');
                },
                function (error) {
                    toastr.error(error.data, 'Falha no registro');
                });
        }

        $scope.closeModal = function (result) {
            dialog.close(result);
        };

        $scope.matchPassword = function () {
            //a senhas combinam
            return $scope.password === $scope.repassword;
        }
    }
})();
