using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarStateHooksInjectorTests
{
    [TestClass]
    public class MethodRendererTests
    {
        [TestMethod]
        public void TestRoslynWorks()
        {
			string testMethod = @""" 
            void MethodA()
            {
                int count = 0;
                count ++;
            }
            """;
			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);

			Assert.IsTrue(methSyntax.Body.Statements.Count == 2);

        }
    }
}
