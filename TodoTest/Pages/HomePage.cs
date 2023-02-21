using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using TodoTest.Helpers;
using FluentAssertions;

namespace TodoTest.Pages
{
    public class HomePage : DriverHelper
    {
        IWebElement todoAppTitle => driver.FindElement(By.XPath("//h1[text()='todos']"));
        IWebElement todoInput => driver.FindElement(By.CssSelector(".new-todo"));
        IWebElement edit => driver.FindElement(By.CssSelector(".edit"));
        IWebElement todoList => driver.FindElement(By.CssSelector(".todo-list"));
        IWebElement counter => driver.FindElement(By.CssSelector(".todo-count"));
        IWebElement destroyButton => driver.FindElement(By.CssSelector(".destroy"));
        IWebElement clearCompletedButton => driver.FindElement(By.CssSelector(".clear-completed"));

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(Constants.Url);
        }

        public bool IsTitleDisplayed()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            try
            {
            return todoAppTitle.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsTodoListDisplayed()
        {
            try
            {
                return todoList.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddTodo(string todo)
        {
            todoInput.SendKeys(todo);
            todoInput.SendKeys(Keys.Enter);
        }

        public bool IsTodoAdded(string todo, int i)
        {
            try
            {
                ExpectedConditions.ElementExists(By.CssSelector($".todo-list li:nth-of-type({i})")); ;
                return driver.FindElement(By.CssSelector($".todo-list li:nth-of-type({i})")).Text.Equals(todo);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void EditTodo(string newTodo, int i)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.CssSelector($".todo-list li:nth-of-type({i})"))).DoubleClick().Perform();
            edit.SendKeys(Keys.Control + "a");
            edit.SendKeys(newTodo);
            edit.SendKeys(Keys.Enter);
        }

        public void CompleteTodo(int i)
        {
            driver.FindElement(By.CssSelector($".todo-list li:nth-of-type({i}) .toggle")).Click();
        }

        public bool IsTodoCompleted(string todo, int i)
        {
            try
            {
                return driver.FindElement(By.CssSelector($".completed:nth-of-type({i})")).Text.Equals(todo);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Counter(string leftItems)
        {
            counter.Text.Equals(leftItems);
        }

        public void DestroyTodo()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.CssSelector($".todo-list li:nth-of-type(1)"))).Perform();
            destroyButton.Click();
        }

        public void ClearCompleted()
        {
            clearCompletedButton.Click();
        }
    }
}
