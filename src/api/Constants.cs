namespace ric.analyzer.api;

public static class Constants {

    public static string APP_NAME = Environment.GetEnvironmentVariable("RIC_APP_NAME") ?? "Roman Imperial Coin Analyzer";
    public static string OTEL_ENDPOINT  = Environment.GetEnvironmentVariable("RIC_OTEL_ENDPOINT") ?? "http://localhost:4317"; 
    public static string OPENAI_ENDPOINT = Environment.GetEnvironmentVariable("RIC_AOI_ENDPOINT") ?? "http://localhost:11434";
    public const string OPENAI_MODEL = "gpt-4-turbo";
    public const string SYSTEM_PROMPT  = @"You are an expert numismatist with a particular focus on Ancient Roman Imperial Coins with a dry sense of humor.
    You have been asked to analyze the following coin.";

    public const string AI_PROMPT = @"What is the inscription on the coin and make a guess on the Emperor depicted? 
    Tell me anything else you can deduce from the coin. It's okay to be wrong.
    These guys are long dead.";
}
