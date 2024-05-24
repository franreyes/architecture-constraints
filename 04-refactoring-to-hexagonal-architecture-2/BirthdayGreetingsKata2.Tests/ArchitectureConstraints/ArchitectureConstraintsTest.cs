using System;
using ArchUnitNET.Domain;
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.NUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using NUnit.Framework;

namespace BirthdayGreetingsKata2.Tests.ArchitectureConstraints;

public class ArchitectureConstraintsTest
{
    private const string AssemblyName = "BirthdayGreetingsKata2";
    private const string BusinessLayer = $"{AssemblyName}.Domain";
    private const string InfrastructureLayer = $"{AssemblyName}.Infrastructure";
    private const string ApplicationLayer = $"{AssemblyName}.App";

    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(System.Reflection.Assembly.Load(AssemblyName))
        .Build();
    
    
    [Test]
    public void Domain_Not_Depends_Infrastructure()
    {
        Types().That().ResideInNamespace(BusinessLayer).Should()
            .NotDependOnAny(Types().That().ResideInNamespace(InfrastructureLayer))
            .Check(Architecture);
    }
    
    [Test]
    public void Domain_Not_Depends_Application()
    {
        Types().That().ResideInNamespace(BusinessLayer).Should()
            .NotDependOnAny(Types().That().ResideInNamespace(ApplicationLayer))
            .Check(Architecture);
    }
    
}