using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Models
{
    public class ActionReturnType
    {
        public HttpStatusCode StatusCode { get; set; }
        public string XTotalCount { get; set; }
        public object ResultSet { get; set; }
        public object ContextData { get; set; }
    }
    public class ActionResultType<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public int? XTotalCount { get; set; }
        public T ResultSet { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public Error(string message, string field)
        {
            this.Message = message;
            this.Field = field;
        }
        public string Message { get; set; }
        public string Field { get; set; }
    }

    public static class ActionSet
    {
        public static ActionReturnType ActionReturnType(HttpStatusCode statusCode, object resultSet = null, int xTotalCount = 0, object contextData = null, string transactionId = "")
        {
            var actionResult = new ActionReturnType
            {
                StatusCode = statusCode,
                ResultSet = resultSet,
                XTotalCount = Convert.ToString(xTotalCount),
                ContextData = contextData
            };
            return actionResult;
        }

        public static ActionResultType<T> ActionResultType<T>(HttpStatusCode statusCode, T resultSet = default(T), int xTotalCount = 0)
        {
            var actionResult = new ActionResultType<T>
            {
                StatusCode = statusCode,
                ResultSet = resultSet,
                XTotalCount = (xTotalCount == 0) ? null : (int?)xTotalCount,
            };
            return actionResult;
        }
        public static ActionResultType<T> ActionResultType<T>(HttpStatusCode statusCode, List<Error> errors)
        {
            var actionResult = new ActionResultType<T>
            {
                StatusCode = statusCode,
                Errors = errors,
                ResultSet = default(T)
            };
            return actionResult;
        }
    }
}
