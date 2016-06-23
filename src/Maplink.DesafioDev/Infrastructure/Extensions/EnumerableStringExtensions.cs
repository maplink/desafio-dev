using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maplink.DesafioDev.Infrastructure.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string ToErrorMessage(this IEnumerable<string> values)
        {
            var errorMessage = new StringBuilder();

            values?
                .ToList()
                .ForEach(item => errorMessage.AppendLine(item));

            return errorMessage.ToString();
        }
    }
}