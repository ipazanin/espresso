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

        stage('First Stage') {
            steps {
                echo "Yay! First stage is executed"
            }
        }
        // stage('Build') {
        //     agent {
        //         docker {
        //             image 'microsoft/dotnet:2.1-sdk'
        //             args '-u root:root'
        //         }
        //     }
        //     steps {
        //         sh 'apt update'
        //         sh 'apt install -y apt-transport-https'
        //         // sh 'echo "{\\\"buildNumber\\\":\\\"${BUILD_NUMBER}\\\", \\\"sha\\\":\\\"need to populate\\\"}" > Jenkins/buildinfo.json'
        //         sh 'echo Hi'
        //         sh 'chmod a+rw -R .'
        //         stash name: 'Jenkins-out', includes: 'Jenkins/out/**'
        //     }
        // }
    }
}