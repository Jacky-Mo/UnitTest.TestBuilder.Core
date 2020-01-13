# UnitTest.TestBuilder.Core

#### Motivation

I have seen a few different inefficient ways of writting unit tests in the past.

###### Example #1: The same piece of code repeated over and over again in different tests. ***Redundant boilerplate code***

```C#
// Repeated code of creating and setting up dependencies
var depA = new Mock<DependencyA>();
var depB = new Mock<DependencyB>();

//set up the other mock dependencies

var testObj = new TestObject(depA, depB, ...depN);

testObj.MethodToTest();
```
 
###### Example #2: Intermingled test setup in the test class that makes unit testsing difficult to maintain

```C#
public class TestClass
{
   // Setup dependencies
   static DependencyA _depA;
   static DependencyB _depB;
   ...
   static DependencyN _depN;

   public void TestMethod()
   {
     // set up data for the dependencies

      var testObj = new TestObject(_depA, _depB, ... _depN); 
      testObj.MethodToTest();
   }
}
```

So, I create this library with the intension to reduce the unit testing boilerplate code and make unit testing setup independent from other tests.


#### How to use
1. Create a TestBuilder class that inherits from the one of the following Builder abstract classes
   
- [UnitTest.TestBuilder.Moq](https://github.com/Jacky-Mo/UnitTest.TestBuilder.Moq)
- [UnitTest.TestBuilder.FakeItEasy](https://github.com/Jacky-Mo/UnitTest.TestBuilder.FakeItEasy)

```C#
public class TestClass
{
  private class Builder : MoqBuilder<TestObject>
  {
  }
}
```

2. Define class properties in the ***Builder*** class. 
> All reference type properties including string will be dynamically created and assigned by implementation of the Test Builder favor.
>
> The ***TestObject*** is created with its public constructor that has the most parameters. If the parameter is the same type of any of the properties defined
> in the ***Builder***, the particular property of the ***Builder*** will be passed in as the parameter to the constructor. Hence, the ***TestObject*** will have access
> to the properties defined in the ***Builder***.
> 
> Each ***Builder*** implementation has its own convention of defining the class properties. Please see the documentation in each respective repo for examples.

3. You can optionally override the creation of the property objects in the constructor of the ***Builder*** class
```C#
  private class Builder : MoqBaseBuilder<TestObject>
  {
      public DependencyA DepA {get; private set;}
      
      public Builder(IContainer container)
      {
         // override the creation of DepA
         DepA = new DepA();
      }
  }
```

4. You can also optionally use dependency injection for the creation of the properties. You can define a custom DI container using your favorite DI library that implements the IContainer interface. This is
useful if you already using a dependency injection and want to reuse some of the dependency setup.

5. You can optionally override the creation of the ***TestObject*** by overriding the __*CreateObject*__ method.


#### How to extend
Please refer to the source code in the two specific implementation repositories on how to create another implementation of your favarite test mocking framework.



<br>
<br>
Last Updated: Jan-12-2020