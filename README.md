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