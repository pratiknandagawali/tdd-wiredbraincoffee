# tdd-wiredbraincoffee

Wired Brain Coffee is a Coffee Shop Company that runs several coffee shops. In several countries, they have desks for customers. We will be using the Wired Brain Coffee shop example to demonstrate the Test Driven Development approach. We will take some of the below requirements and start building using the Test Driven Development approach. In combination with the TDD approach, we are going to use SOLID principles to build our application more cleaner, scalable and maintainable. 

1.On the Web Page, the customer can enter FirstName, LastName, Email, and Date. These same inputs need to be returned to return.

2.If the Request is Null, the API should throw an Argument Null Exception.

3.Save a Desk Booking into the Database.

4.Check If a desk is Available.

5.Send Success or NoDesk Available.

To accomplish the above business needs we will be using the Test Driven Development approach.

Test-driven development (TDD) is a software development process relying on software requirements being converted to test cases before software is fully developed, and tracking all software development by repeatedly testing the software against all test cases.

**Add a test**

The adding of a new feature begins by writing a test that passes iff the feature's specifications are met. The developer can discover these specifications by asking about use cases and user stories. A key benefit of test-driven development is that it makes the developer focus on requirements before writing code. This is in contrast with the usual practice, where unit tests are only written after code.

**Run all tests.** 

The new test should fail for expected reasons This shows that new code is actually needed for the desired feature. It validates that the test harness is working correctly. It rules out the possibility that the new test is flawed and will always pass.

**Write the simplest code that passes the new test Inelegant or hard code is acceptable**, as long as it passes the test. The code will be honed anyway in Step 5. No code should be added beyond the tested functionality.

All tests should now pass If any fail, the new code must be revised until they pass. This ensures the new code meets the test requirements and does not break existing features.

**Refactor** as needed, using tests after each refactor to ensure that functionality is preserved Code is refactored for readability and maintainability. In particular, hard-coded test data should be removed. Running the test suite after each refactor helps ensure that no existing functionality is broken.

**Examples of refactoring:** moving code to where it most logically belongs, removing duplicate code, making names self-documenting, splitting methods into smaller pieces, and re-arranging inheritance hierarchies

**Repeat**

The cycle above is repeated for each new piece of functionality. Tests should be small and incremental, and commits made often. That way, if new code fails some tests, the programmer can simply undo or revert rather than debug excessively. When using external libraries, it is important not to write tests that are so small as to effectively test merely the library itself, unless there is some reason to believe that the library is buggy or not feature-rich enough to serve all the needs of the software under development.
