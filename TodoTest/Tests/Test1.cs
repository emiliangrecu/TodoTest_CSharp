using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TodoTest.Helpers;
using TodoTest.Pages;

namespace TodoTest.Tests
{
    public class Tests : DriverHelper
    {
        HomePage homePage = new HomePage();
        private string todo1 = "run a test";
        private string todo2 = "take a pause";
        private string todo3 = "drink water";
        private string editedtodo1 = "edited todo1";
        private string editedtodo2 = "edited todo2";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void VerifyIfTodosCanBeAdded()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.IsTodoAdded(todo1, 1).Should().BeTrue();
            homePage.AddTodo(todo2);
            homePage.IsTodoAdded(todo2, 2).Should().BeTrue();
            homePage.AddTodo(todo3);
            homePage.IsTodoAdded(todo3, 3).Should().BeTrue();
            homePage.Counter("3 items left");
        }

        [Test]
        public void VerifyIfTodosCanBeEdited()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.AddTodo(todo2);
            homePage.EditTodo(editedtodo1, 1);
            homePage.EditTodo(editedtodo2, 2);
            homePage.IsTodoAdded(editedtodo1, 1).Should().BeTrue();
            homePage.IsTodoAdded(editedtodo2, 2).Should().BeTrue();
        }

        [Test]
        public void VerifyIfTodosCanBeCompleted()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.CompleteTodo(1);
            homePage.IsTodoCompleted(todo1, 1).Should().BeTrue();
            homePage.AddTodo(todo2);
            homePage.CompleteTodo(2);
            homePage.IsTodoCompleted(todo2, 2).Should().BeTrue();
            homePage.Counter("0 items left");
        }

        [Test]
        public void VerifyIfTodosCanBeRemoved()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.AddTodo(todo2);
            homePage.Counter("2 items left");
            homePage.CompleteTodo(2);
            homePage.Counter("1 item left");
            homePage.DestroyTodo();
            homePage.Counter("0 items left");
        }

        [Test]
        public void VerifyThereAreNoTodosWhenOpeningPage()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.IsTodoListDisplayed().Should().BeFalse();
        }

        [Test]
        public void VerifyThereAreNoExtraTodosWhenAddingOne()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.IsTodoAdded(todo2, 2).Should().BeFalse();
        }

        [Test]
        public void VerifyThereAreNoTodoAfterClearingTheCompletedTodos()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.CompleteTodo(1);
            homePage.AddTodo(todo2);
            homePage.CompleteTodo(2);
            homePage.ClearCompleted();
            homePage.IsTodoListDisplayed().Should().BeFalse();
        }

        [Test]
        public void VerifyThereAreNoTodosAfterCompletingAndRemoving()
        {
            homePage.GoToPage();
            homePage.IsTitleDisplayed();

            homePage.AddTodo(todo1);
            homePage.AddTodo(todo2);
            homePage.CompleteTodo(2);
            homePage.AddTodo(todo3);

            homePage.ClearCompleted();
            for (int i = 1; i <= 2; i++)
            {
                homePage.DestroyTodo();
            }
            homePage.IsTodoListDisplayed().Should().BeFalse();
        }

        [TearDown]
        public void After()
        {
            driver.Quit();
        }

    }
}
