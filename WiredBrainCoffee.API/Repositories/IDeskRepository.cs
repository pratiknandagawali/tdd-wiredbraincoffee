namespace WiredBrainCoffee.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using WiredBrainCoffee.API.Domain;

    public interface IDeskRepository
    {
        IEnumerable<Desk> GetAvailableDesks(DateTime date);
    }
}
