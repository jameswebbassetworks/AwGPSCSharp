using CSharpInterviewMessageProcessor;
using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Logging;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure Serilog for console and file logging
        Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Console()
               .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day)
               .CreateLogger();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });

        Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("Vehicle Message Processor started.");

        Console.WriteLine("Vehicle Message Processor");
        Console.WriteLine("=========================");
        Console.Write("Enter the path to the message file: ");

        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            logger.LogError("File path cannot be empty.");
            Console.WriteLine("Error: File path cannot be empty.");
            return;
        }

        if (!File.Exists(filePath))
        {
            logger.LogError("File not found at {FilePath}", filePath);
            Console.WriteLine($"Error: File not found at '{filePath}'");
            return;
        }

        var messageQueue = new MessageQueue();
        var cancellationTokenSource = new CancellationTokenSource();

        // Consumer thread - processes messages from queue
        var consumerThread = new Thread(() =>
        {
            var processor = new MessageProcessor();
            try
            {
                logger.LogInformation("Consumer thread started.");
                while (!messageQueue.IsCompleted)
                {
                    try
                    {
                        var message = messageQueue.Dequeue(cancellationTokenSource.Token);
                        logger.LogInformation("Dequeued message: {Message}\n", message);
                        processor.Process(message);
                    }
                    catch (OperationCanceledException)
                    {
                        logger.LogWarning("Operation cancelled while dequeuing messages.");
                        break;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                logger.LogWarning("Message queue completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error in consumer thread.");
            }
        });

        consumerThread.Start();

        // Producer - reads file and enqueues messages
        try
        {
            logger.LogInformation("Reading XML content from file: {FilePath}", filePath);
            var xmlContent = File.ReadAllText(filePath);

            var parser = new MessageParser();
            logger.LogInformation("Parsing XML content into messages.");
            var messages = parser.ParseAll(xmlContent);

            foreach (var message in messages)
            {
                messageQueue.Enqueue(message);
                logger.LogDebug("Enqueued message: {Message}", message);
            }

            messageQueue.CompleteAdding();
            logger.LogInformation("All messages enqueued. Waiting for consumer thread to finish.");

            consumerThread.Join();
            logger.LogInformation("All {Count} message(s) processed successfully.", messages.Count);
            Console.WriteLine($"All {messages.Count} message(s) processed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing messages: {Message}", ex.Message);
            Console.WriteLine($"Error processing messages: {ex.Message}");
            cancellationTokenSource.Cancel();
        }
        finally
        {
            logger.LogInformation("Disposing cancellation token source and shutting down.");
            cancellationTokenSource.Dispose();
            logger.LogInformation("Vehicle Message Processor finished execution.");
        }
    }
}