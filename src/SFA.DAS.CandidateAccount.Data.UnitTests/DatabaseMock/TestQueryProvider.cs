using System.Collections;
using System.Linq.Expressions;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock
{
    public abstract class TestQueryProvider<T> : IOrderedQueryable<T>, IQueryProvider
    {
        private IEnumerable<T> _enumerable;

        protected TestQueryProvider(Expression expression, TestQueryExecutionLog executionLog)
        {
            Expression = expression;
            ExecutionLog = executionLog;
        }

        protected TestQueryProvider(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
            Expression = enumerable.AsQueryable().Expression;
            ExecutionLog = new TestQueryExecutionLog();
        }

        public TestQueryExecutionLog ExecutionLog { get; }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression is MethodCallExpression m)
            {
                var resultType = m.Method.ReturnType; // it should be IQueryable<T>
                var tElement = resultType.GetGenericArguments().First();
                return (IQueryable)CreateInstance(tElement, expression);
            }

            return CreateQuery<T>(expression);
        }

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
        {
            return (IQueryable<TEntity>)CreateInstance(typeof(TEntity), expression);
        }

        private object CreateInstance(Type tElement, Expression expression)
        {
            var queryType = GetType().GetGenericTypeDefinition().MakeGenericType(tElement);
            return Activator.CreateInstance(queryType, expression, ExecutionLog);
        }

        public object Execute(Expression expression)
        {
            return CompileExpressionItem<object>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression is MethodCallExpression { Method.Name: "ExecuteDelete" or "ExecuteUpdate" } methodCall)
            {
                var affectedEntities = CompileExpressionItem<IEnumerable>(methodCall.Arguments[0]).Cast<object>().ToList();
                if (methodCall.Method.Name == "ExecuteDelete")
                {
                    ExecutionLog.ExecuteDeleteCalls.Add(affectedEntities);
                }

                object affectedRows = affectedEntities.Count;
                return (TResult)affectedRows;
            }

            return CompileExpressionItem<TResult>(expression);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (_enumerable == null)
                _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_enumerable == null)
                _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);
            return _enumerable.GetEnumerator();
        }

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider => this;

        private static TResult CompileExpressionItem<TResult>(Expression expression)
        {
            var visitor = new TestExpressionVisitor();
            var body = visitor.Visit(expression);
            var f = Expression.Lambda<Func<TResult>>(
                body ?? throw new InvalidOperationException($"{nameof(body)} is null"),
                (IEnumerable<ParameterExpression>)null);
            return f.Compile()();
        }
    }
}
