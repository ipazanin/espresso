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
    }
}