pipeline {
    agent any

    options {
        skipDefaultCheckout true
    } 

    stages {
        stage('checkout') {
            steps {
                checkout scm
            }
        }
        stage('Clean') {
            steps {
                bash "dotnet clean source\\Espresso.sln"
             }
        }
        stage('Restore packages') {
            steps {
                bash "dotnet restore source\\Espresso.sln"
            }
        }
        stage('Build') {
            steps {
                bash "dotnet build source\\Espresso.sln --configuration Release"
            }
        }
        stage('Test: Unit Test') {
            steps {
                bash "dotnet test source\\Espresso.sln"
            }
        }
        stage('Publish') {
            steps {
                bash "dotnet publish source\\Espresso.sln"
            }
        }
         
    }
}