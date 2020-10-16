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
                bat "dotnet clean source\\Espresso.sln"
            }
        }
    }
}