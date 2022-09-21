using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NortwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var db = new NortwindContext())
            {
                var result = (from operationClaim in db.OperationClaims
                             join userOperationClaim in db.UserOperationClaims
                              on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             });
                return result.ToList();
            }
        }
    }
}
