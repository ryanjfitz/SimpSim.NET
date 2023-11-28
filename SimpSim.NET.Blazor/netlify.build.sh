#!/usr/bin/env bash
set -e

pushd /tmp
wget https://dot.net/v1/dotnet-install.sh
chmod u+x /tmp/dotnet-install.sh
/tmp/dotnet-install.sh --channel 8.0
popd

dotnet publish -c Release -o release