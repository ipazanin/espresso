pipeline {
    agent any

    stages {
        stage('Clean') {
            steps {
                sh "dotnet clean source/Espresso.sln"
            }
        }
    }
}