(function () {
    //reclama tudo o que tiver errado
    'use strict';

    angular
        .module('app')
        //definindo módulo
        .factory('UserRepository', UserRepository);

    UserRepository.$inject = ['$http', '$rootScope', '$location'];

    function UserRepository($http, $rootScope, $location) {
        return {
            setCurrentProfile: function () {
                return $http.get("/api/account/profile", { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') } })
                    .then(
                    function (result) {
                        $rootScope.user = {
                            name: result.data.name,
                            email: result.data.email,
                            theme: result.data.theme
                        };
                    },
                    function (error) {
                        ClearUserData();
                    });
            },
            clearUserData: function () {
                ClearUserData();
            },
            register: function (user) {
                return $http.post("/api/account/register", user, { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') } });
            },
            resetPassword: function (email) {
                return $http.post("/api/account/resetpassword", email, { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') } });
            }
        };

        function ClearUserData() {
            $rootScope.isAuthorized = false;
            $rootScope.user = {
                name: '',
                email: ''
            };
            localStorage.setItem('token', '');
            $location.path('/login');
        }
    }
})();