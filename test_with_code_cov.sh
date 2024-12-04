#!/usr/bin/env bash

dotnet clean
dotnet build -c Release
dotnet test -c Release
