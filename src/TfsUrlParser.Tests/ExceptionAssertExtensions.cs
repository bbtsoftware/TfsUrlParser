namespace TfsUrlParser.Tests
{
    using System;
    using Xunit;

    public static class ExceptionAssertExtensions
    {
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName);
        }

        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Equal(parameterName, ((ArgumentOutOfRangeException)exception).ParamName);
        }

        public static void IsUriFormatExceptionException(this Exception exception, string message)
        {
            Assert.IsType<UriFormatException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
