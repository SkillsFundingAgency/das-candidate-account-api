﻿using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock
{
    public class TestAsyncEnumerableEfCore<T> : TestQueryProvider<T>, IAsyncEnumerable<T>, IAsyncQueryProvider
    {
        public TestAsyncEnumerableEfCore(Expression expression) : base(expression)
        {
        }

        public TestAsyncEnumerableEfCore(IEnumerable<T> enumerable) : base(enumerable)
        {
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                .GetMethods()
                .First(method => method.Name == nameof(IQueryProvider.Execute) && method.IsGenericMethod)
                .MakeGenericMethod(expectedResultType)
                .Invoke(this, new object[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                .MakeGenericMethod(expectedResultType)
                .Invoke(null, new[] { executionResult });
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}
