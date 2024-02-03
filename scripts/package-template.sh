#!/bin/bash

set -e

cleanup() {
  echo "Wystąpił błąd. Wycofywanie zmian..."
  rm -rf ./src/$name
  exit 1
}

while getopts n:f: flag
do
  case "${flag}" in
    n) name=${OPTARG};;
    f) framework=${OPTARG};;
    *) echo 'Option not exists. Use -n to provide package name and -f to provide framework.'; exit 1;;
  esac
done

if [ -z "$name" ] || [ -z "$framework" ]; then
  echo "Nazwa pakietu i framework są wymagane."
  exit 1
fi

# Create project structure
(
mkdir -p ./src/$name/src && mkdir -p ./src/$name/scripts && mkdir -p ./src/$name/tests \
    && cd ./src/$name/tests && touch .gitkeep && cd .. \
    && cp ../../scripts/bin-obj-clear.sh ./scripts \
    && cp ../../scripts/dotnet-build-all.sh ./scripts \
    && cp ../../scripts/dotnet-restore-all.sh ./scripts \
    && cp ../../scripts/grant-privileges.sh ./scripts \
    && ./scripts/grant-privileges.sh && cd ../..
) || cleanup

# Create .NET project
(
cd ./src/$name/src && dotnet new classlib -n $name -f $framework --force \
    && rm -f $name/Class1.cs \
    && mkdir $name/Global \
    && touch $name/Global/Usings.cs \
    && cp ../../../templates/template.csproj $name/$name.csproj \
    && sed -i '' "s/NAME/$name/g" $name/$name.csproj \
    && sed -i '' "s/FRAMEWORK/$framework/g" $name/$name.csproj \
    && cat $name/$name.csproj
) || cleanup

# Add project to sln
(
  cd $PWD && dotnet sln add ./src/$name/src/$name/$name.csproj \
  && dotnet sln list
) || cleanup
