/// <reference path="account/views/signup.html" />
/// <reference path="account/views/signup.html" />
(function () {
    'use strict';
    var id = 'app';

    //criação de módulo
    //nome e suas dependencias em []
    var app = angular.module('app', ['ngResource', 'ngRoute', 'ngAnimate', 'ui.bootstrap']);

    //definição das configurações
    app.config(function ($routeProvider, $locationProvider) {
        $routeProvider
            //quando a url for : /
             .when('/', {
                 //carrega controller
                 controller: 'HomeCtrl',
                 //carrega view
                 templateUrl: 'modules/home/views/index.html',
                 //só acessa a home se estiver logado = true
                 requireLogin: true
             })
             .when('/account/login', {
                 controller: 'LoginCtrl',
                 templateUrl: 'modules/account/views/login.html',
                 requireLogin: false
             })
             .when('/account/logout', {
                 controller: 'LogoutCtrl',
                 templateUrl: 'modules/account/views/logout.html',
                 requireLogin: false
             })
             .when('/account/signup', {
                 controller: 'SignUpCtrl',
                 templateUrl: 'modules/account/views/signup.html',
                 requireLogin: false
             })
             .when('/account/forgot-password', {
                 controller: 'ForgotPasswordCtrl',
                 templateUrl: 'modules/account/views/forgot-password.html',
                 requireLogin: false
             })
             .otherwise({
                 controller: 'HomeCtrl as vm',
                 templateUrl: 'modules/home/views/404.html',
                 requireLogin: true
             });
    });

    //módulo
    //Scope = controller específico
    //rootScope = aplicação em geral
    app.run(['$http', '$rootScope', '$location', 'UserRepository', function ($http, $rootScope, $location, UserRepository) {
        // Usuário logado        
        $rootScope.isAuthorized = false;
        $rootScope.user = {
            name: '',
            email: ''
        };

        // Verifica se não existe um token
        if (!localStorage.getItem('token')) {
            //se não tem gera vazio
            localStorage.setItem('token', '');
        } else {//autorizado 
            $rootScope.isAuthorized = true;
            UserRepository.setCurrentProfile();
        }

        // Verifica o tema
        if (!localStorage.getItem('theme')) {
            localStorage.setItem('theme', 'journal');
            $rootScope.theme = 'journal';
        } else {
            $rootScope.theme = localStorage.getItem('theme');
        }
        
        //verifica se a próxima rota requer autenticação
        $rootScope.$on("$routeChangeStart", function (event, next, current) {
            if (next.requireLogin && $rootScope.isAuthorized == false) {
                $location.path('/account/login');
            }
        });
    }]);
})();