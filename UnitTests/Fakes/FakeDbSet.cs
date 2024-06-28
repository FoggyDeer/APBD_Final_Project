using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APBD_Final_Project.UnitTests.Fakes;

public class FakeDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity> where TEntity : class
{
    private readonly List<TEntity> _data;
    private readonly IQueryable<TEntity> _query;
    public FakeDbSet()
    {
        _data = new List<TEntity>();
        _query = _data.AsQueryable();
    }

    public override IEntityType EntityType { get; }

    public Type ElementType => _query.ElementType;
    public Expression Expression => _query.Expression;

    public IEnumerator<TEntity> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

    public override ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        _data.Add(entity);
        return new ValueTask<EntityEntry<TEntity>>();
    }
}