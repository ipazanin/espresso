pipeline {
    agent any

    options {
        skipDefaultCheckout true
    }

    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
    }    

    stages {
        stage('checkout') {
            steps {
                checkout scm
            }
        }
        stage('Clean') {
            steps {
                bat "dotnet clean source\\Espresso.sln"
             }
        }
        stage('Restore packages') {
            steps {
                bat "dotnet restore source\\Espresso.sln"
            }
        }
        stage('Build') {
            steps {
                bat "dotnet build source\\Espresso.sln --configuration Release"
            }
        }
        stage('Test: Unit Test') {
            steps {
                bat "dotnet test source\\Espresso.sln"
            }
        }
        stage('Publish') {
            steps {
                bat "dotnet publish YourProjectPath\\Your_Project.csproj "
            }
        }
         
    }
}