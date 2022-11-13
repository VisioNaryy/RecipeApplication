using Polly.Retry;

namespace RecipeApplication.Policies;

public interface IClientPolicy
{
    AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
    AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
    AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
}