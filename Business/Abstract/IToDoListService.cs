using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IToDoListService
    {
        IDataResult<List<ToDoList>> GetAll();
        IDataResult<ToDoList> GetById(int id);
        IDataResult<List<ToDoList>> GetAllByUserId(int userId);
        IResult Add(ToDoList toDoList);
        IResult Update(ToDoList toDoList);
        IResult Delete(ToDoList toDoList);

    }
}
