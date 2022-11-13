using Polly;
using Polly.Retry;

namespace RecipeApplication.Policies;

public class ClientPolicy : IClientPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

    public ClientPolicy()
    {
        ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode).RetryAsync(5);

        LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));
        
        ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1.35, retryAttempt)));
    }
}