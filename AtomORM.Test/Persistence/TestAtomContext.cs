using System.Data.Common;
using AtomORM.Core;

namespace AtomORM.Test.Persistence;

    public class TestAtomContext : AtomContext
    {
        public TestAtomContext(AtomContextOptions options) : base(options) {}

        public AtomEntity<Person> stringProperty { get; set; }
        public string AmazingString { get; set; }
    }   