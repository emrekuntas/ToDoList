using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        IToDoListService _toDoListService;

        public ToDoListsController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _toDoListService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _toDoListService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getallbyuserid")]
        public IActionResult GetAllByUserId(int userId)
        {
            var result = _toDoListService.GetAllByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(ToDoList toDo)
        {
            var result = _toDoListService.Add(toDo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(ToDoList toDo)
        {
            var result = _toDoListService.Update(toDo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(ToDoList toDo)
        {
            var result = _toDoListService.Delete(toDo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
