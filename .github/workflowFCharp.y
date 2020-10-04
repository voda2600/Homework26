name: .NET Core

on:
  push:
    branches: [ master ]
  pull_request:
    branches: [ master ]

defaults:
  run:
    working-directory: ./CalculatorC#/
    
jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.301
    - name: Install dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --configuration Release --no-restore
    - name: Test
      run: dotnet test -c Release --no-build  UnitTestProject1/CalcTest.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
    - name: Upload coverage to Codecov
      uses: codecov/codecov-action@v1
      with:
        token: ${{ secrets.CODECOV_TOKEN }}
        file: CalculatorC#/UnitTestProject1/coverage.opencover.xml
        flags: tests
        name: codecov-umbrella
        yml: ./codecov.yml
        fail_ci_if_error: true
