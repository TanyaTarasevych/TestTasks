using System.Text;
using APItestsTask3.Models;

namespace APItestsTask3.Utils
{
    public class QueryBuilder
    {
        private readonly StringBuilder _stringBuilder;

        public QueryBuilder()
        {
            _stringBuilder = new StringBuilder("?");
        }

        public QueryBuilder WithParameter(string key, string value)
        {
            if (_stringBuilder.Length != 1)
            {
                _stringBuilder.Append('&');
            }

            _stringBuilder.Append($"{key}={value}");

            return this;
        }

        public QueryBuilder WithParameters(IEnumerable<Query> queryParameters)
        {
            ArgumentNullException.ThrowIfNull(queryParameters);

            foreach (var queryParameter in queryParameters)
            {
                WithParameter(queryParameter.Field!, queryParameter.Value!);
            }

            return this;
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}
