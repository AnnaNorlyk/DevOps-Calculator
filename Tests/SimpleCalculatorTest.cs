using Calculator;

namespace Tests;

public class SimpleCalculatorTest
{
    [Test]
    public void Add()
    {
        // Arrange
        var calc = new SimpleCalculator();
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
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Subtract(2, 5);

        // Assert
        Assert.That(result, Is.EqualTo(-3));
    }


    [Test]
    public void Multiply()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Multiply(4, 3);

        // Assert
        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void Divide()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Divide(10, 2);

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    }

    [Test]
    public void Factorial_PositiveNumber()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Factorial(5);

        // Assert
        Assert.That(result, Is.EqualTo(120));
    }

    [Test]
    public void Factorial_Zero()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Factorial(0);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void Factorial_NegativeNumber_ThrowsException()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var ex = Assert.Throws<ArgumentException>(() => calc.Factorial(-1));

        //Assert
        Assert.That(ex.Message, Is.EqualTo("Factorial is not defined for negative numbers"));
    }

    [Test]
    public void IsPrime_PrimeNumber()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.IsPrime(5);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPrime_NotPrimeNumber()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.IsPrime(4);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPrime_One()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.IsPrime(1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPrime_Two()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.IsPrime(2);

        // Assert
        Assert.That(result, Is.True);
    }
}