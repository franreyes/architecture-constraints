using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.NUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using NUnit.Framework;

namespace BirthdayGreetingsKata2.Tests.ArchitectureConstraints;

public class ArchitectureConstraintsTest
{
    private const string AssemblyName = "BirthdayGreetingsKata2";
    private const string BusinessLayer = $"{AssemblyName}.Business";
    private const string InfrastructureLayer = $"{AssemblyName}.Infrastructure.*";
    private const string ApplicationLayer = $"{AssemblyName}.Application.*";

    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(System.Reflection.Assembly.Load(AssemblyName))
        .Build();
    
    
    [Test]
    public void Business_Should_Not_Depend_On_Infrastructure()
    {
        Types().That().ResideInNamespace(BusinessLayer).Should()
            .NotDependOnAny(Types().That().ResideInNamespace(InfrastructureLayer, true))
            .Check(Architecture);
    }
    
    [Test]
    public void Business_Should_Not_Depend_On_Application_Layer()
    {
        Types().That().ResideInNamespace(BusinessLayer).Should()
            .NotDependOnAny(Types().That().ResideInNamespace(ApplicationLayer, true))
            .Check(Architecture);
    }
}