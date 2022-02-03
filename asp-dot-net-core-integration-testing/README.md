# ASP.Net Core integration testing

## Set up Test Project

- Add a xUnit Test Project
- Add [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing) package to the test project
- Change the `Sdk` in the project file from `Microsoft.NET.Sdk` to `Microsoft.NET.Sdk.Web`. Using this `Sdk` ensures that we get the best build experience and all of the features we need will be enabled(Steve Gordon - [Integration Testing ASP.NET Core Applications: Best Practices](https://www.pluralsight.com/courses/integration-testing-asp-dot-net-core-applications-best-practices)).

  ```xml
  <Project Sdk="Microsoft.NET.Sdk.Web">
   <!--removed for brevity-->
  </Project>
  ```

- Add `xunit.runner.json` file to the project folder
- Add `shadowCopy` property to `xunit.runner.json` file and set it to `false`. When the `shadowCopy` is enabled(by default), tests will execute in a different directory than build output. For integration tests to run correctly we need this disabled(Steve Gordon - [Integration Testing ASP.NET Core Applications: Best Practices](https://www.pluralsight.com/courses/integration-testing-asp-dot-net-core-applications-best-practices)).

  ```json
  {
    "shadowCopy": false
  }
  ```

## Resources

- [Microsoft Docs - Integration tests in ASP.NET Core](https://docs.microsoft.com/aspnet/core/test/integration-tests)
- [Microsoft Docs - Test ASP.NET Core MVC apps](https://docs.microsoft.com/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)
- [Microsoft Docs - WebApplicationFactory Class (Microsoft.AspNetCore.Mvc.Testing)](https://docs.microsoft.com/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1)
- [Sending and Receiving JSON using HttpClient with System.Net.Http.Json by Steve Gordon](https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json)
