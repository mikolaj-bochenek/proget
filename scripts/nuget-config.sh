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
    <add key="github" value="https://nuget.pkg.github.com/Progmat-Company/index.json" />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="$username" />
      <add key="ClearTextPassword" value="$pat" />
    </github>
  </packageSourceCredentials>
</configuration>
EOT
