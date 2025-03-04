using StreamService.Business.Abstract;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

public class MembershipManager(IMembershipDal membershipDal) : IMembershipService
{
    private readonly IMembershipDal _membershipDal = membershipDal;

    public Task<bool> ActivateAsync(string id)
    {
        return _membershipDal.ActivateAsync(id);
    }

    public Task<Membership> CreateAsync(Membership entity)
    {
        return _membershipDal.CreateAsync(entity);
    }

    public Task<bool> DeactivateAsync(string id)
    {
        return _membershipDal.DeactivateAsync(id);
    }

    public Task<List<Membership>> GetAllAsync()
    {
        return _membershipDal.GetAllAsync();
    }

    public Task<Membership> GetByIdAsync(string id)
    {
        return _membershipDal.GetByIdAsync(id);
    }

    public Task<Membership> UpdateAsync(Membership entity)
    {
        return _membershipDal.UpdateAsync(entity);
    }
}
