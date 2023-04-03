using KursDbInlm.Contexts;
using KursDbInlm.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace KursDbInlm.Services;

internal class CaseService
{
    private readonly DataContext _context = new DataContext();


    //Create a case/report
    public async Task CreateAsync(CaseEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .ToListAsync();
    }

    /*public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _entity = await _context.Cases
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(predicate);
        return _entity!;
    }*/

    public async Task<CaseEntity> GetAsync(int caseId)
    {
        var caseEntity = await _context.Cases           
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == caseId);
        if (caseEntity != null)
            return caseEntity;

        return null!;
    }




    //update status on specifik report
    public async Task UpdateCaseStatusAsync(int caseId, int StatusId)
    {
        var _entity = await _context.Cases.FindAsync(caseId);
        if (_entity != null) 
        {
            _entity.StatusId = StatusId;
            _context.Update(_entity);
            await _context.SaveChangesAsync();
        }
    }





    /*public async Task CreateCommentAsync(CommentEntity comment)
    {
        await _context.AddAsync(comment);
        await _context.SaveChangesAsync();
        await UpdateCaseStatusAsync(comment.CaseId, 2);
    }*/


}
