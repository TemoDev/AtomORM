using AtomORM.Core;

namespace AtomORM.Abstractions;

public interface IAtomEntity
{
    uint Id { get; set; }
    EntityState State { get; set; }
}