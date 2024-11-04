using MockMe.Tests.SampleClasses;

namespace MockMe.Tests;

public class GenericMethodTests
{
    [Fact]
    public void GenericMethod_DefinedInDifferentAssembly_ShouldBeMockedSuccessfully()
    {
        var ilReplacedObject = new MockMe.SampleMocks.CalculatorSample.Calculator();
        var randomReplaced = ilReplacedObject.AddUpAllOfThese2<double>(
            0,
            new double[] { 1, 2, 3, 4, 5 },
            0
        );

        var ilDefinitionObject = new Calculator2();
        var randomSource = ilDefinitionObject.AddUpAllOfThese2<double>(
            0,
            new double[] { 1, 2, 3, 4, 5 },
            0
        );

        Assert.Equal(randomSource, randomReplaced);
    }
}
