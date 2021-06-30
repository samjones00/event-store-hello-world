﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LoyaltyCard.Core
{
    public abstract class Aggregate
    {
        readonly IList<object> _changes = new List<object>();

        public Guid Id { get; protected set; } = Guid.Empty;
        public long Version { get; private set; } = -1;

        protected abstract void When(object e);

        public void Apply(object e)
        {
            When(e);
            _changes.Add(e);
        }

        public void Load(long version, IEnumerable<object> history)
        {
            Version = version;
            foreach (var e in history)
            {
                When(e);
            }
        }

        public object[] GetChanges() => _changes.ToArray();
    }
}
