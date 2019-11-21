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
1. Create a TestBuilder class that inherits from the one of the following TestBuilder abstract classes
   
- [UnitTest.TestBuilder.Moq](https://github.com/Jacky-Mo/UnitTest.TestBuilder.Moq)
- [UnitTest.TestBuilder.FakeItEasy](https://github.com/Jacky-Mo/UnitTest.TestBuilder.FakeItEasy)

```C#
public class TestClass
{
  private class TestBuilder : MoqTestBuilder<TestObject>
  {
  }
}
```

2. Define class properties in the ***TestBuilder*** class. 
> All reference type properties other than string will be dynamically created and assigned by the TestBuilder favor implementation.
>
> The ***TestObject*** is created with its public constructor that has the most parameters. If the parameter is the same type of any of the properties defined
> in the ***TestBuilder***, the property of the ***TestBuilder*** will be passed in as the parameter to the constructor. Hence, the ***TestObject*** will have reference
> to the properties defined in the ***TestBuilder***.
> 
> Each ***TestBuilder*** implementation has its own convention of defining the class properties. Please see the documentation in each respective repo for examples.

3. You can optionally override the creation of the property objects in the constructor of the ***TestBuilder*** class
```C#
  private class TestBuilder : MoqTestBuilder<TestObject>
  {
      public DependencyA DepA {get; private set;}
      
      public TestBuilder(IContainer container)
      {
         // override the creation of DepA
         DepA = new DepA();
      }
  }
```

4. You can also optionally use dependency injection for the creation of the properties. You can define a custom DI container using your favorite DI library that implements the IContainer interface.

5. You can optionally override the creation of the ***TestObject*** by overriding the *CreateObject* method.


#### How to extend
Please refer to the source code in the two specific implementation repositories on how to create another favor implementation of your favarite mocking framework.



<br>
<br>
Last Updated: 11/21/2019