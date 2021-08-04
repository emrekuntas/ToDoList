using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace ToDoListUnitTest.Test
{
    public class ToDoListApiControllerTest
    {
        private readonly Mock<IToDoListService> _mockRepo;
        private readonly ToDoListsController _controller;

        private IDataResult<List<ToDoList>> DataResultToDoLists;
        private List<ToDoList> toDoList;

        public ToDoListApiControllerTest()
        {
            _mockRepo = new Mock<IToDoListService>();
            _controller = new ToDoListsController(_mockRepo.Object);

            toDoList = new List<ToDoList>()
            {
                new ToDoList { Id=1,Title="testMoc",Description="TestMoc",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,IsComplete=false,IsDeleted=false,UserId=1},
                new ToDoList { Id=2,Title="testMoc2",Description="TestMoc2",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,IsComplete=false,IsDeleted=false,UserId=1}
            };
            DataResultToDoLists = new SuccessDataResult<List<ToDoList>>(toDoList);
        }
        [Fact]
        public void GetAll_ActionExecutes_ReturnOkResultWithToDoLists()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetAll()).Returns(DataResultToDoLists);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnToDoList = Assert.IsAssignableFrom<SuccessDataResult<List<ToDoList>>>(okResult.Value);

            Assert.Equal<int>(2, returnToDoList.Data.Count);
        }
        [Theory]
        [InlineData(0)]
        public void GetById_ActionExecutes_ReturnBadResultWhenToDoNotFound(int toDoId)
        {
            // Arrange
            ToDoList tempNullToDolist = null;
            IDataResult<ToDoList> nullToDoDataResult = new ErrorDataResult<ToDoList>(tempNullToDolist);
            _mockRepo.Setup(x => x.GetById(toDoId)).Returns(nullToDoDataResult);

            // Act
            var result = _controller.GetById(toDoId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ActionExecutes_ReturnOkResultWhenTodoFound(int toDoId)
        {
            // Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IDataResult<ToDoList> toDoDataResult = new SuccessDataResult<ToDoList>(toDo);
            _mockRepo.Setup(x => x.GetById(toDoId)).Returns(toDoDataResult);

            // Act
            var result = _controller.GetById(toDoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTodo = Assert.IsAssignableFrom<SuccessDataResult<ToDoList>>(okResult.Value);
            Assert.Equal(toDoId, returnTodo.Data.Id);
            Assert.Equal(toDo.Title, returnTodo.Data.Title);
        }
        [Theory]
        [InlineData(0)]
        public void GetAllByUserId_ActionExecutes_ReturnBadResultWhenUserNotFound(int userId)
        {
            // Arrange
            List<ToDoList> tempNullToDolist = null;
            IDataResult<List<ToDoList>> nullToDoDataResult = new ErrorDataResult<List<ToDoList>>(tempNullToDolist);
            _mockRepo.Setup(x => x.GetAllByUserId(userId)).Returns(nullToDoDataResult);

            // Act
            var result = _controller.GetAllByUserId(userId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByUserId_ActionExecutes_ReturnOkResultWhenUserFound(int userId)
        {
            // Arrange
            var toDo = DataResultToDoLists.Data.Where(x => x.UserId == userId).ToList();
            IDataResult<List<ToDoList>> toDoDataResult = new SuccessDataResult<List<ToDoList>>(toDo);
            _mockRepo.Setup(x => x.GetAllByUserId(userId)).Returns(toDoDataResult);

            // Act
            var result = _controller.GetAllByUserId(userId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnToDoList = Assert.IsAssignableFrom<SuccessDataResult<List<ToDoList>>>(okResult.Value);

        }

        [Theory]
        [InlineData(1)]
        public void UpdateToDoList_ActionExecutes_BadRequestWhenTodoContentIncorrect(int toDoId)
        {
            //Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult updatedData = new Result(false);
            _mockRepo.Setup(x => x.Update(toDo)).Returns(updatedData);

            //Act
            var result = _controller.Update(toDo);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Theory]
        [InlineData(1)]
        public void UpdateToDoList_ActionExecutes_ReturnOkResultWhenDataUpdated(int toDoId)
        {
            //Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult updatedData = new Result(true);
            _mockRepo.Setup(x => x.Update(toDo)).Returns(updatedData);

            //Act
            var result = _controller.Update(toDo);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        
        [Theory]
        [InlineData(1)]
        public void AddToDoList_ActionExecutes_BadRequestWhenTodoContentIncorrect(int toDoId)
        {
            ////Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult addedData = new Result(false);
            _mockRepo.Setup(x => x.Add(toDo)).Returns(addedData);

            ////Act
            var result = _controller.Add(toDo);

            ////Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Theory]
        [InlineData(1)]
        public void AddToDoList_ActionExecutes_ReturnOkResultWhenDataAdded(int toDoId)
        {
            //Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult addedData = new Result(true);
            _mockRepo.Setup(x => x.Add(toDo)).Returns(addedData);

            //Act
            var result = _controller.Add(toDo);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData(1)]
        public void DeleteToDoList_ActionExecutes_BadRequestWhenTodoContentIncorrect(int toDoId)
        {
            ////Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult deletedData = new Result(false);
            _mockRepo.Setup(x => x.Delete(toDo)).Returns(deletedData);

            ////Act
            var result = _controller.Delete(toDo);

            ////Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Theory]
        [InlineData(1)]
        public void DeleteToDoList_ActionExecutes_ReturnOkResultWhenDataDeleted(int toDoId)
        {
            ////Arrange
            var toDo = DataResultToDoLists.Data.SingleOrDefault(x => x.Id == toDoId);
            IResult deletedData = new Result(true);
            _mockRepo.Setup(x => x.Delete(toDo)).Returns(deletedData);

            ////Act
            var result = _controller.Delete(toDo);

            ////Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}