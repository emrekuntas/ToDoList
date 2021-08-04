using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user), "Roller listelendi.");
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserRegistered);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            if (user == null)
            {
                return new ErrorDataResult<User>(user, Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(user, "Email bilgisine göre kullanıcı çelildi.");
        }
    }
}

