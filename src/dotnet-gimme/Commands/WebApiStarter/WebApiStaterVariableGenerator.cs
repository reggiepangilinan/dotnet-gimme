namespace dotnetgimme.Commands.WebApiStarter
{
    public class WebApiStaterVariableGenerator
    {
        readonly string solutionName;

        public WebApiStaterVariableGenerator(string solutionName)
        {
            this.solutionName = solutionName;
        }

        public string SolutionName => solutionName;
        public string SolutionFilePath => $"{SolutionName}/{SolutionName}.sln";
        public string WebApiProject => $"{SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj";
        public string ApplicationProject => $"{SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj";
        public string ApplicationUnitTestProject => $"{SolutionName}/{SolutionName}.Application.UnitTest/{SolutionName}.Application.UnitTest.csproj";
        public string ServicesProject => $"{SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj";
        public string DomainProject => $"{SolutionName}/{SolutionName}.Domain/{SolutionName}.Domain.csproj";
        public string PersistanceProject => $"{SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj";
    }
}
