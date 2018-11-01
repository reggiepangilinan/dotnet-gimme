using System;
using System.IO;

namespace gimme.Commands.WebApiStarter
{
    public class VariableGenerator
    {
        private readonly string solutionName;

        public VariableGenerator(string solutionName)
        {
            this.solutionName = solutionName;
        }


        public string SolutionName => solutionName;
        public string ApiProjectName => $"{solutionName}.Api";
        public string ApplicationProjectName => $"{solutionName}.Application";
        public string ApplicationUnitTestProjectName => $"{solutionName}.ApplicationUnitTest";
        public string ServicesProjectName => $"{solutionName}.Services";
        public string DomainProjectName => $"{solutionName}.Domain";
        public string PersistenceProjectName => $"{solutionName}.Persistence";

        public string SolutionBasePath => Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/");

        public string GimmeSettingsJsonPath => Path.Combine(SolutionBasePath, "gimmesettings.json");

        public string SLN_SolutionFile => $"{SolutionName}/{SolutionName}.sln";
        public string CSPROJ_WebApiProjectFile => $"{SolutionName}/{ApiProjectName}/{ApiProjectName}.csproj";
        public string CSPROJ_ApplicationProjectFile => $"{SolutionName}/{ApplicationProjectName}/{ApplicationProjectName}.csproj";
        public string CSPROJ_ApplicationUnitTestProjectFile => $"{SolutionName}/{ApplicationUnitTestProjectName}/{ApplicationUnitTestProjectName}.csproj";
        public string CSPROJ_ServicesProjectFile => $"{SolutionName}/{ServicesProjectName}/{ServicesProjectName}.csproj";
        public string CSPROJ_DomainProjectFile => $"{SolutionName}/{DomainProjectName}/{DomainProjectName}.csproj";
        public string CSPROJ_PersistenceProjectFile => $"{SolutionName}/{PersistenceProjectName}/{PersistenceProjectName}.csproj";


    }
}
