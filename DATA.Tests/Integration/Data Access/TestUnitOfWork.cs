using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DATA.Repository.Configuration;
using DATA.Repository.Abstraction;
using System;
using DATA.Repository.Abstraction.ConcurrencyHandling;

namespace DATA.Tests.Integration
{
    public class TestUnitOfWork<TKey> : UnitOfWork<TestContext>
    {
        public TestUnitOfWork(TestContext context) : base(context)
        {
            
        }
    }


}











