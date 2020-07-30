#!/bin/zsh
function read_solution() {
    echo "Parsing solution $1"

    setopt local_options BASH_REMATCH

    while IFS='' read -r line || [[ -n "$line" ]]; do
            if [[ $line =~ \"([^\"]*.csproj)\" ]]; then
                    project="${BASH_REMATCH[1]}"
                    
                    read_project "$(echo "$project"|tr '\\' '/')"
            fi
    done < "$1"
    echo "Done parsing solution"
}

function read_project() {
    echo "Parsing project $1"

    setopt local_options BASH_REMATCH
    package_regex='PackageReference Include="([^"]*)" Version="([^"]*)"'
    
    while IFS='' read -r line || [[ -n "$line" ]]; do
            if [[ $line =~ $package_regex ]]; then
                    name="${BASH_REMATCH[2]}"
                    version="${BASH_REMATCH[3]}"
                    if [[ $version != *-* ]]; then
                            dotnet add "$1" package "$name"
                    fi
            fi
    done < $1
}

function dotnet_update_packages() {
    has_read=0

    if [[ $1 =~ \.sln$ ]]; then
            read_solution "$1"
            return 0
    elif [[ $1 =~ \.csproj$ ]]; then
            read_project "$1"
            return 0
    elif [[ $1 != "" ]]; then
            echo "Invalid file $1"
            return 1
    fi


    for solution in ./*.sln; do
            if [ ! -f ${solution} ]; then
                    continue
            fi

            read_solution "${solution}"
            has_read=1
    done

    if [[ $has_read -eq 1 ]]; then
            return 0
    fi

    for project in ./*.csproj; do
            if [ ! -f ${project} ]; then
                    continue
            fi

            read_project "${project}"
    done
}

function dotnet_restore_espresso(){
    dotnet restore ./source/Espresso.Application/Espresso.Application.csproj
    dotnet restore ./source/Espresso.DataAccessLayer/Espresso.DataAccessLayer.csproj
    dotnet restore ./source/Espresso.Common/Espresso.Common.csproj
    dotnet restore ./source/Espresso.Domain/Espresso.Domain.csproj
    dotnet restore ./source/Espresso.Persistence/Espresso.Persistence.csproj
    dotnet restore ./source/Espresso.WebApi/Espresso.WebApi.csproj
    dotnet restore ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj
}

function dotnet_update_packages_espresso(){
    dotnet_update_packages ./source/Espresso.Application/Espresso.Application.csproj
    dotnet_update_packages ./source/Espresso.DataAccessLayer/Espresso.DataAccessLayer.csproj
    dotnet_update_packages ./source/Espresso.Common/Espresso.Common.csproj
    dotnet_update_packages ./source/Espresso.Domain/Espresso.Domain.csproj
    dotnet_update_packages ./source/Espresso.Persistence/Espresso.Persistence.csproj
    dotnet_update_packages ./source/Espresso.WebApi/Espresso.WebApi.csproj
    dotnet_update_packages ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj
}

# restore
dotnet_restore_espresso

# update packages
dotnet_update_packages_espresso

# restore 
dotnet_restore_espresso