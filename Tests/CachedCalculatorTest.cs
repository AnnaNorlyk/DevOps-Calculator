using Calculator;

namespace Tests;

public class CachedCalculatorTest
{
    [Test]
    public void Add()
    {
        // Arrange
        var calc = new CachedCalculator();
        var a = 2;
        var b = 3;

        // Act
        var result = calc.Add(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Subtract()
    {
        // Arrange
        var calc = new CachedCalculator();
        var a = 5;
        var b = 2;

        // Act
        var result = calc.Subtract(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void Multiply()
    {
        // Arrange
        var calc = new CachedCalculator();
        var a = 4;
        var b = 3;

        // Act
        var result = calc.Multiply(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void Divide()
    {
        // Arrange
        var calc = new CachedCalculator();
        var a = 10;
        var b = 2;

        // Act
        var result = calc.Divide(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Factorial_ValidNumber()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 5;

        // Act
        var result = calc.Factorial(n);

        // Assert
        Assert.That(result, Is.EqualTo(120));
    }

    [Test]
    public void Factorial_NegativeNumber_ThrowsException()
    {
        // Arrange
        var calc = new CachedCalculator();
        var invalidNumber = -1;

        // Act
        var ex = Assert.Throws<ArgumentException>(() => calc.Factorial(invalidNumber));

        //Assert
        Assert.That(ex.Message, Is.EqualTo("Factorial is not defined for negative numbers"));
    }

    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 0;

        // Act
        var result = calc.Factorial(n);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void Factorial_Zero_IsLessThanTwo()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 0;

        // Act
        var result = calc.Factorial(n);

        // Assert
        Assert.That(result, Is.LessThan(2));
    }

    [Test]
    public void IsPrime_PrimeNumber_True()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 5;

        // Act
        var result = calc.IsPrime(n);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPrime_One_False()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 1;

        // Act
        var result = calc.IsPrime(n);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPrime_Two_True()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 2;

        // Act
        var result = calc.IsPrime(n);

        // Assert
        Assert.That(result, Is.True);
    }
}