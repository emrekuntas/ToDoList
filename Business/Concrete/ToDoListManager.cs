using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    [SecuredOperation("user")]
    public class ToDoListManager : IToDoListService
    {
        IToDoListDal _toDoListDal;

        public ToDoListManager(IToDoListDal toDoListDal)
        {
            _toDoListDal = toDoListDal;
        }
        
        [SecuredOperation("todo.add")]
        [ValidationAspect(typeof(ToDoValidator))]
        public IResult Add(ToDoList toDoList)
        {
            toDoList.CreatedOn = DateTime.Now;
            toDoList.IsDeleted = false;
            toDoList.IsComplete = false;
            _toDoListDal.Add(toDoList);
            return new SuccessResult(Messages.ToDoAssigmentAdded);
        }
        public IResult Delete(ToDoList toDoList)
        {
            _toDoListDal.Delete(toDoList);
            return new SuccessResult(Messages.ToDoAssigmentDeleted);
        }
        public IDataResult<List<ToDoList>> GetAll()
        {
            var result = _toDoListDal.GetAll();
            return new SuccessDataResult<List<ToDoList>>(result, Messages.ToDoAssigmentsListed);
        }
        public IDataResult<List<ToDoList>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<ToDoList>>(_toDoListDal.GetAll(t => t.UserId == userId), Messages.UserToDoAssigmentsListed);

        }
        public IDataResult<ToDoList> GetById(int id)
        {
            return new SuccessDataResult<ToDoList>(_toDoListDal.Get(t => t.Id == id), Messages.ToDoAssigmentDetail);
        }

        [ValidationAspect(typeof(ToDoValidator))]
        public IResult Update(ToDoList toDoList)
        {
            toDoList.ModifiedOn = DateTime.Now;
            _toDoListDal.Update(toDoList);
            return new SuccessResult(Messages.ToDoAssigmentUpdated);
        }
    }
}
