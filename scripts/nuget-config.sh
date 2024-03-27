#!/bin/bash

display_help() {
  echo "Usage: $0 [options]"
  echo "Options:"
  echo "  -u    Repository username"
  echo "  -p    Repository Personal Access Token (PAT)"
  echo "  -h    Show this help message"
}

while getopts u:p:h: option; do
  case $option in
    u) username=${OPTARG};;
    p) pat=${OPTARG};;
    h) display_help; exit 0;;
    *) display_help; exit 1;;
  esac
done

if [ -z "$username" ] || [ -z "$pat" ]; then
  echo "Missing required options."; display_help; exit 1;
fi

cat <<EOT > nuget.config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="github-progmat-company" value="https://nuget.pkg.github.com/progmat-company/index.json" />
    <add key="github-esensic" value="https://nuget.pkg.github.com/esensic/index.json" />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github-progmat-company>
      <add key="Username" value="$username" />
      <add key="ClearTextPassword" value="$pat" />
    </github-progmat-company>
    <github-esensic>
      <add key="Username" value="$username" />
      <add key="ClearTextPassword" value="$pat" />
    </github-esensic>
  </packageSourceCredentials>
</configuration>
EOT