namespace Microsoft.SemanticKernel.ChatCompletion;

public interface IChatHistoryReducer
{
    Task<IEnumerable<ChatMessageContent>> ReduceAsync(IReadOnlyList<ChatMessageContent> chatHistory, CancellationToken cancellationToken = default);
}