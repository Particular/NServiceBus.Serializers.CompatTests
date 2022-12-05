namespace Common.Tests.TestCases.Types
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Polymorphic
    {
        public List<BaseEntity> Items { get; set; }
    }

    [Serializable]
    public class BaseEntity
    {
        public virtual string Name { get; set; }
    }

    [Serializable]
    public class SpecializationA : BaseEntity
    {
        public override string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SpecializationA other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }

    [Serializable]
    public class SpecializationB : BaseEntity
    {
        public override string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SpecializationB other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}