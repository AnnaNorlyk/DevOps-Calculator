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


        // Act
        var result = calc.Subtract(2, 5);

        // Assert
        Assert.That(result, Is.EqualTo(-3));
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

    [Test]
    public void IsPrime_Four_False()
    {
        // Arrange
        var calc = new CachedCalculator();
        var n = 4;

        // Act
        calc.IsPrime(n);
        var result = calc.IsPrime(n);

        // Assert
        Assert.That(result, Is.False); 
    }



    //Mutants
    [Test]
    public void Factorial_One()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Factorial(1);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void Factorial_LargeNumber()
    {
        // Arrange
        var calc = new SimpleCalculator();

        // Act
        var result = calc.Factorial(6);

        // Assert
        Assert.That(result, Is.EqualTo(720));
    }

    [Test]
    public void Add_CachesResult()
    {
        // Arrange
        var calc = new CachedCalculator();
        var x = 7;
        var y = 4;

        // Act
        calc.Add(x, y);

        // Assert
        Assert.That(calc.Cache, Has.Count.EqualTo(1));
    }

    [Test]
    public void Add_ReturnsCachedValue()
    {
        // Arrange
        var calc = new CachedCalculator();
        var a = 6;
        var b = 2;
        calc.Add(a, b); 

        // Act
        var result = calc.Add(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]
    public void Add_HandlesMultipleInputs()
    {
        // Arrange
        var calc = new CachedCalculator();
        var p = 10;
        var l = 5;
        var m = 2;
        calc.Add(p, l);
        calc.Add(l, m); 

        // Act
        var result = calc.Add(l, m);

        // Assert
        Assert.That(result, Is.EqualTo(7)); 
    }

[Test]
    public void Subtract_CachesResult()
    {
        var calc = new CachedCalculator();
        calc.Subtract(10, 3);
        Assert.That(calc.Cache.Count, Is.EqualTo(1));
    }

    [Test]
    public void Subtract_ReturnsCachedValue()
    {
        var calc = new CachedCalculator();
        calc.Subtract(10, 3);
        var result = calc.Subtract(10, 3);
        Assert.That(result, Is.EqualTo(7));
    }

    [Test]
    public void Multiply_CachesResult()
    {
        var calc = new CachedCalculator();
        calc.Multiply(4, 3);
        Assert.That(calc.Cache.Count, Is.EqualTo(1));
    }

    [Test]
    public void Multiply_ReturnsCachedValue()
    {
        var calc = new CachedCalculator();
        calc.Multiply(4, 3);
        var result = calc.Multiply(4, 3);
        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void Divide_CachesResult()
    {
        var calc = new CachedCalculator();
        calc.Divide(10, 2);
        Assert.That(calc.Cache.Count, Is.EqualTo(1));
    }

    [Test]
    public void Divide_ReturnsCachedValue()
    {
        var calc = new CachedCalculator();
        calc.Divide(10, 2);
        var result = calc.Divide(10, 2);
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Factorial_CachesResult()
    {
        var calc = new CachedCalculator();
        calc.Factorial(5);
        Assert.That(calc.Cache.Count, Is.EqualTo(1));
    }

    [Test]
    public void Factorial_ReturnsCachedValue()
    {
        var calc = new CachedCalculator();
        calc.Factorial(5);
        var result = calc.Factorial(5);
        Assert.That(result, Is.EqualTo(120));
    }
}