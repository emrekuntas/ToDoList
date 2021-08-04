using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ToDoValidator: AbstractValidator<ToDoList>
    {
        public ToDoValidator()
        {
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MinimumLength(2);
            RuleFor(p => p.Description).MinimumLength(5);
        }
    }
}
